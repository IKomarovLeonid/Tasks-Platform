using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Environment.State;
using MediatR;
using Objects.Common;
using Objects.Dto;
using Persistence.Src;
using Persistence.Storage;
using State.Commands.Tasks;
using TaskStatus = Objects.Dto.TaskStatus;

namespace State.Handlers.Tasks
{
    internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, StateResult>
    {
        // services
        private readonly IDomainManager<TaskDto> _storage;

        public CreateTaskHandler(IDomainManager<TaskDto> storage)
        {
            _storage = storage;
        }

        public async Task<StateResult> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var dto = new TaskDto()
            {
                Title = command.Title,
                Description = command.Description,
                ExpirationUtc = command.ExpirationUtc,
                IsActive = true,
                Status = TaskStatus.Pending,
                State = RootState.Active
            };

            var result = dto.Validate();
            if(!result.IsValid) return StateResult.Error(ErrorCode.TaskValidationFailure, result.Errors.First().ToString());

            var entity = await _storage.AddAsync(dto);

            return StateResult.Applied(entity.Id);
        }
    }
}
