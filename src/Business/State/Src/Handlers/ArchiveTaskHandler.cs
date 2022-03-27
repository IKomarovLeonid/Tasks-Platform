using MediatR;
using State.Commands;
using System.Threading;
using System.Threading.Tasks;
using Objects.Common;
using Objects.Dto;
using Persistence.Storage;
using TaskStatus = Objects.Dto.TaskStatus;

namespace State.Handlers
{
    internal class ArchiveTaskHandler : IRequestHandler<ArchiveTaskCommand, StateResult>
    {
        // services
        private readonly IStorage<TaskDto> _storage;

        public ArchiveTaskHandler(IStorage<TaskDto> storage)
        {
            _storage = storage;
        }


        public async Task<StateResult> Handle(ArchiveTaskCommand request, CancellationToken cancellationToken)
        {
            var dto = await _storage.FindByIdAsync(request.Id);

            if (dto == null) return StateResult.Error(ErrorCode.NotFound);

            if (!dto.IsActive) return StateResult.Error(ErrorCode.TaskArchived);

            dto.IsActive = false;
            dto.Status = TaskStatus.Processed;
            dto.State = RootState.Archived;

            var entity = await _storage.UpdateAsync(dto);

            return StateResult.Applied(entity.Id);
        }
    }
}
