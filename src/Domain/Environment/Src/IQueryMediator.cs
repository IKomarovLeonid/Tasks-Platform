using System;
using System.Threading.Tasks;
using Queries.Find;
using Queries.Select;

namespace Environment.Src
{
    public interface IQueryMediator
    {
        Task<FindResult<TModel>> FindAsync<TModel>(FindQuery<TModel> query);

        Task<SelectResult<TModel>> SelectAsync<TModel>(SelectQuery<TModel> query);
    }
}
