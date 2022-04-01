using System;
using System.Threading;
using System.Threading.Tasks;
using Environment.State;
using MediatR;
using Newtonsoft.Json;
using Objects.Settings;
using Persistence.Storage;
using State.Commands.Settings;

namespace State.Src.Handlers.Settings
{
    public class SetDefaultSettingsHandler : IRequestHandler<SetDefaultSettingsCommand, StateResult>
    {
        private readonly ISettingsStorage<BaseSettings> _storage;

        public SetDefaultSettingsHandler(ISettingsStorage<BaseSettings> storage)
        {
            _storage = storage;
        }

        public async Task<StateResult> Handle(SetDefaultSettingsCommand request, CancellationToken cancellationToken)
        {
            var jobs = await _storage.FindAsync(SettingsType.Jobs.ToString());

            if (jobs == null) await SetDefaultJobsAsync();

            return StateResult.Applied();
        }

        private async Task SetDefaultJobsAsync()
        {
            var settings = new JobSettings()
            {
                CheckTaskExpirationJobSec = 50
            };

            var baseSettings = new BaseSettings()
            {
                Key = SettingsType.Jobs.ToString(),
                Value = JsonConvert.SerializeObject(settings)
            };

            await _storage.UpdateAsync(baseSettings);
        }
    }
}
