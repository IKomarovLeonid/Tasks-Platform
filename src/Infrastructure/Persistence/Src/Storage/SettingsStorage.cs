using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Objects;

namespace Persistence.Storage
{
    public class SettingsStorage<TModel> : ISettingsStorage<TModel> where TModel: class, ISettings
    {
        private readonly IServiceScopeFactory _factory;

        public SettingsStorage(IServiceScopeFactory factory)
        {
            _factory = factory;
        }

        public async Task<TModel> FindAsync(string key)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var entities = context.Set<TModel>().AsNoTracking();

            return await entities.FirstOrDefaultAsync(t => t.Key == key);
        }

        public async Task<TModel> UpdateAsync(TModel model)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var entry = await context.Set<TModel>().FirstOrDefaultAsync(t => t.Key == model.Key);
        
            if (entry == null)
            {
                model.UpdatedUtc = DateTime.UtcNow;
                var output = await context.Set<TModel>().AddAsync(model);
                await context.SaveChangesAsync();
                return output.Entity;
            }
            model.UpdatedUtc = DateTime.UtcNow;
            var entryEntry = context.Entry(entry);
            entryEntry.CurrentValues.SetValues(model);
            await context.SaveChangesAsync();

            return entryEntry.Entity;
        }
    }
}
