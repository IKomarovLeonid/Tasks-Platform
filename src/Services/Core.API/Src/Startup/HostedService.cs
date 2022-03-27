using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace Core.API.Startup
{
    internal class HostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public HostedService(IServiceScopeFactory factory)
        {
            _scopeFactory = factory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await MigrateAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task MigrateAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            await db.Database.EnsureCreatedAsync(cancellationToken);
        }

    }
}
