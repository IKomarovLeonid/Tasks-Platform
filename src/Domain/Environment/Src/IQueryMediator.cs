using System.Threading.Tasks;
using Environment.Queries;

namespace Environment
{
    public interface IQueryMediator
    {
        Task<FindResult<TModel>> FindAsync<TModel>(FindQuery<TModel> query);

        Task<SelectResult<TModel>> SelectAsync<TModel>(SelectQuery<TModel> query);
    }
}
