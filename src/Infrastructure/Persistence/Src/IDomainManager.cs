using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Objects;

namespace Persistence.Src
{
    public interface IDomainManager<TModel> where TModel : class, IDto
    {
        public Task<TModel> AddAsync(TModel model);

        public Task<TModel> FindByIdAsync(ulong id);

        public Task<ICollection<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression = null);

        public Task<TModel> UpdateAsync(TModel model);

        public Task ReloadStoreAsync();
    }
}
