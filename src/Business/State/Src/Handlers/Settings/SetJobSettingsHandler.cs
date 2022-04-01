using System.Linq;
using MediatR;
using State.Commands.Settings;
using System.Threading;
using System.Threading.Tasks;
using Environment.State;
using Newtonsoft.Json;
using Objects.Common;
using Objects.Settings;
using Persistence.Storage;

namespace State.Handlers.Settings
{
    internal class SetJobSettingsHandler : IRequestHandler<SetJobsCommand, StateResult>
    {
        private readonly ISettingsStorage<BaseSettings> _storage;

        public SetJobSettingsHandler(ISettingsStorage<BaseSettings> storage)
        {
            _storage = storage;
        }

        public async Task<StateResult> Handle(SetJobsCommand request, CancellationToken cancellationToken)
        {
            var settings = new JobSettings()
            {
                CheckTaskExpirationJobSec = request.CheckTaskExpirationJobSec
            };

            var validation = settings.Validate();
            if(!validation.IsValid) return StateResult.Error(ErrorCode.SettingsValidationFailure, $"{validation.Errors.First()}");

            var baseSettings = new BaseSettings()
            {
                Key = SettingsType.Jobs.ToString(),
                Value = JsonConvert.SerializeObject(settings)
            };

            await _storage.UpdateAsync(baseSettings);

            return StateResult.Applied();
        }
    }
}
