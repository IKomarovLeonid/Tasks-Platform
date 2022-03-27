﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Objects;

namespace Persistence.Storage
{
    public class Storage<TModel> : IStorage<TModel> where TModel : class, IDto
    {
        private readonly IServiceScopeFactory _factory;

        public Storage(IServiceScopeFactory factory)
        {
            _factory = factory;
        }


        public async Task<TModel> AddAsync(TModel model)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var time = DateTime.UtcNow;
            model.CreatedUtc = time;
            model.UpdatedUtc = time;

            var entity = await context.Set<TModel>().AddAsync(model);

            await context.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task<TModel> UpdateAsync(TModel model)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var entry = await context.Set<TModel>().FirstAsync(t => t.Id == model.Id);

            model.UpdatedUtc = DateTime.UtcNow;

            var entryEntry = context.Entry(entry);
            entryEntry.CurrentValues.SetValues(model);
            await context.SaveChangesAsync();

            return entryEntry.Entity;
        }

        public async Task<ICollection<TModel>> GetAllAsync(Expression<Func<TModel, bool>> query = null)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var entities = context.Set<TModel>().AsNoTracking();
            if (query != null)
            {
                return await entities.Where(query).ToListAsync();
            }

            return await entities.ToListAsync();
        }

        public async Task<TModel> FindByIdAsync(ulong id)
        {
            using var scope = _factory.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var entities = context.Set<TModel>().AsNoTracking();

            return await entities.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
