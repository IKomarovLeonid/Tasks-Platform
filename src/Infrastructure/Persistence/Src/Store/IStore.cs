using System.Collections.Generic;

namespace Persistence.Src.Store
{
    public interface IStore<TModel> where TModel : class
    {
        bool Set(ulong id, TModel model);

        ICollection<TModel> GetAll();

        TModel FindByIdOrDefault(ulong id);

        bool Remove(ulong id);

        void Clear();
    }
}
