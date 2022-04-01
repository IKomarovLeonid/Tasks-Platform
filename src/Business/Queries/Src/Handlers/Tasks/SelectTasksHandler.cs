using System.Threading;
using System.Threading.Tasks;
using Environment.Queries;
using MediatR;
using Objects.Common;
using Objects.Dto;
using Persistence.Storage;

namespace Queries.Handlers.Tasks
{
    internal class SelectTasksHandler : IRequestHandler<SelectQuery<TaskDto>, SelectResult<TaskDto>>
    {
        // services
        private readonly IStorage<TaskDto> _storage;

        public SelectTasksHandler(IStorage<TaskDto> storage)
        {
            _storage = storage;
        }

        public async Task<SelectResult<TaskDto>> Handle(SelectQuery<TaskDto> request, CancellationToken cancellationToken)
        {
            var data = await _storage.GetAllAsync(request.Scope == VisibleScope.Active
                ? t => t.State == RootState.Active
                : null);

            return SelectResult<TaskDto>.Fetched(data);
        }
    }
}
