using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Src.Store
{
    public class Store<TModel> : IStore<TModel> where TModel: class
    {
        private readonly ConcurrentDictionary<ulong, TModel> _data;

        public Store()
        {
            _data = new ConcurrentDictionary<ulong, TModel>();
        }

        public TModel FindByIdOrDefault(ulong id)
        {
            if(_data.TryGetValue(id, out var data))
            {
                return data;
            }
            return null;
        }

        public ICollection<TModel> GetAll()
        {
            return _data.Values.ToList();
        }

        public bool Set(ulong id, TModel model)
        {
            var isExists = _data.TryGetValue(id, out var actualModel);

            if (!isExists)
            {
                return _data.TryAdd(id, model);
            }

            return _data.TryUpdate(id, model, actualModel);
        }

        public bool Remove(ulong id) => _data.TryRemove(id, out var model);

        public void Clear()
        {
            _data.Clear();
        }
    }
}
