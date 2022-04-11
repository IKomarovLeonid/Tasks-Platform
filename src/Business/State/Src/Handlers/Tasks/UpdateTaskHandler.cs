using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Environment.State;
using MediatR;
using Objects.Common;
using Objects.Dto;
using Persistence.Src;
using State.Commands.Tasks;
using TaskStatus = Objects.Dto.TaskStatus;

namespace State.Handlers.Tasks
{
    internal class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, StateResult>
    {
        // services
        private readonly IDomainManager<TaskDto> _storage;

        public UpdateTaskHandler(IDomainManager<TaskDto> storage)
        {
            _storage = storage;
        }

        public async Task<StateResult> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var dto = await _storage.FindByIdAsync(request.Id);

            if(dto == null) return StateResult.Error(ErrorCode.NotFound);

            if(request.Title != null) dto.Title = request.Title;
            if(request.Description != null) dto.Description = request.Description;
            if(request.ExpirationUtc.HasValue) dto.ExpirationUtc = request.ExpirationUtc.Value;
            if(request.Status.HasValue && request.Status.Value != TaskStatus.NotDefined) dto.Status = request.Status.Value;

            var result = dto.Validate();
            if (!result.IsValid) return StateResult.Error(ErrorCode.TaskValidationFailure, result.Errors.First().ToString());

            // additional: set expiration to null when task are 'done'
            if (dto.Status == TaskStatus.Processed) dto.ExpirationUtc = null;

            var entity = await _storage.UpdateAsync(dto);

            return StateResult.Applied(entity.Id);
        }
    }
}
