using MediatR;
using Objects.Settings;
using System.Threading;
using System.Threading.Tasks;
using Environment.Queries;
using Newtonsoft.Json;
using Objects.Common;
using Persistence.Storage;

namespace Queries.Handlers.Settings
{
    internal class FindJobsSettingsHandler : IRequestHandler<FindQuery<JobSettings>, FindResult<JobSettings>>
    {
        private readonly ISettingsStorage<BaseSettings> _storage;

        public FindJobsSettingsHandler(ISettingsStorage<BaseSettings> storage)
        {
            _storage = storage;
        }

        public async Task<FindResult<JobSettings>> Handle(FindQuery<JobSettings> request, CancellationToken cancellationToken)
        {
            var settings = await _storage.FindAsync(SettingsType.Jobs.ToString());

            if (settings == null) return FindResult<JobSettings>.Error(ErrorCode.NotFound);

            var model = JsonConvert.DeserializeObject<JobSettings>(settings.Value);

            return FindResult<JobSettings>.Ok(model);
        }
    }
}
