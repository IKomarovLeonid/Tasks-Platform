using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Environment.Events;
using Objects;

namespace Persistence.Storage
{
    public interface IStorage<TModel> where TModel : class, IDto
    {
        Task<TModel> AddAsync(TModel model);

        Task<TModel> UpdateAsync(TModel model);

        Task<ICollection<TModel>> GetAllAsync(Expression<Func<TModel, bool>> query = null);

        Task<TModel> FindByIdAsync(ulong id);

        IDisposable Subscribe(Action<StateEvent<TModel>> subscriber);
    }
}
