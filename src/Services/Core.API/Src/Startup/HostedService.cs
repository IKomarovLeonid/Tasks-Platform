using System;
using System.Threading;
using System.Threading.Tasks;
using Environment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Persistence;
using Scheduler.Src;
using State.Commands.Settings;

namespace Core.API.Startup
{
    internal class HostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private static readonly ILogger _logger = LogManager.GetLogger(nameof(HostedService));

        public HostedService(IServiceScopeFactory factory)
        {
            _scopeFactory = factory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await MigrateAsync(cancellationToken);
                await RunDefaultCommandsAsync(cancellationToken);
                await StartJobsAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                await StopJobsAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private async Task MigrateAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            await db.Database.EnsureCreatedAsync(cancellationToken);
        }

        private async Task StartJobsAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<QuartzService>();

            await service.StartAsync(cancellationToken);
        }

        private async Task StopJobsAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<QuartzService>();

            await service.StopAsync(cancellationToken);
        }

        private async Task RunDefaultCommandsAsync(CancellationToken token)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IDomainMediator>();

            _logger.Info("Run default settings command...");

            await mediator.SendAsync(new SetDefaultSettingsCommand());

            _logger.Info("Run default settings command has been finished");
        }
    }
}
