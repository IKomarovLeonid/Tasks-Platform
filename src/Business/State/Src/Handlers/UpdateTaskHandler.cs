using MediatR;
using Objects.Dto;
using Persistence.Storage;
using State.Commands;
using System.Threading;
using System.Threading.Tasks;
using Objects.Common;

namespace State.Handlers
{
    internal class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, StateResult>
    {
        // services
        private readonly IStorage<TaskDto> _storage;

        public UpdateTaskHandler(IStorage<TaskDto> storage)
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
            if(request.Status.HasValue) dto.Status = request.Status.Value;

            var entity = await _storage.UpdateAsync(dto);

            return StateResult.Applied(entity.Id);
        }
    }
}
