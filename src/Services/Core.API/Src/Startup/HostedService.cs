using System;
using System.Threading;
using System.Threading.Tasks;
using Environment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Persistence;
using Processing;
using Scheduler;
using State.Commands.Settings;

namespace Core.API.Startup
{
    internal class HostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private static readonly ILogger Logger = LogManager.GetLogger(nameof(HostedService));

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
                StartListeners();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
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
                Logger.Error(ex);
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

            Logger.Info("Run default settings command...");

            await mediator.SendAsync(new SetDefaultSettingsCommand());

            Logger.Info("Run default settings command has been finished");
        }

        private void StartListeners()
        {
            Logger.Info("Listeners will start...");

            using var scope = _scopeFactory.CreateScope();

            var listeners = scope.ServiceProvider.GetServices<IListener>();

            foreach(var listener in listeners)
            {
                listener.Start();
            }

            Logger.Info("Listeners has been started");
        }
    }
}
