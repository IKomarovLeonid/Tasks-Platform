using MediatR;

namespace Queries.Find
{
    public class FindQuery<TModel> : IRequest<FindResult<TModel>>
    {
        public ulong Id { get; }

        public FindQuery(ulong id)
        {
            Id = id;
        }

        public FindQuery(){}
    }
}
