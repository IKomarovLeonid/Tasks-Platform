using MediatR;
using Objects.Common;

namespace Queries.Select
{
    public class SelectQuery<TModel> : IRequest<SelectResult<TModel>>
    {
        public VisibleScope Scope { get; }

        public SelectQuery(VisibleScope scope)
        {
            Scope = scope;
        }
    }
}
