using MediatR;
using Objects.Dto;
using Persistence.Storage;
using System.Threading;
using System.Threading.Tasks;
using Objects.Common;
using Queries.Find;

namespace Queries.Handlers.Tasks
{
    internal class FindTaskHandler : IRequestHandler<FindQuery<TaskDto>, FindResult<TaskDto>>
    {
        // services
        private readonly IStorage<TaskDto> _storage;

        public FindTaskHandler(IStorage<TaskDto> storage)
        {
            _storage = storage;
        }

        public async Task<FindResult<TaskDto>> Handle(FindQuery<TaskDto> request, CancellationToken cancellationToken)
        {
            var dto = await _storage.FindByIdAsync(request.Id);

            return dto == null ? FindResult<TaskDto>.Error(ErrorCode.NotFound) : FindResult<TaskDto>.Ok(dto);
        }
    }
}
