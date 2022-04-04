using System.Threading.Tasks;
using NLog;
using Objects.Dto;
using Persistence.Src;
using Quartz;

namespace Processing.Jobs
{
    public class ReloadCachesJob : IJob
    {
        // services
        private readonly IDomainManager<TaskDto> _storage;

        private readonly ILogger Logger = LogManager.GetLogger(nameof(ReloadCachesJob));

        public ReloadCachesJob(IDomainManager<TaskDto> storage)
        {
            _storage = storage;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Logger.Info("job will reload caches...");

            await _storage.ReloadStoreAsync();

            Logger.Info("Caches has been reloaded");
        }
    }
}
