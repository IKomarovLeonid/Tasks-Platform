using MediatR;
using Objects.Dto;
using Persistence.Storage;
using State.Commands;
using System.Threading;
using System.Threading.Tasks;
using Objects.Common;
using TaskStatus = Objects.Dto.TaskStatus;

namespace State.Handlers
{
    internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, StateResult>
    {
        // services
        private readonly IStorage<TaskDto> _storage;

        public CreateTaskHandler(IStorage<TaskDto> storage)
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

            var entity = await _storage.AddAsync(dto);

            return StateResult.Applied(entity.Id);
        }
    }
}
