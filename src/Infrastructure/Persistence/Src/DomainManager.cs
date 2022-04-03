using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Objects;
using Objects.Common;
using Persistence.Src.Store;
using Persistence.Storage;

namespace Persistence.Src
{
    public class DomainManager<TModel> : IDomainManager<TModel> where TModel: class, IDto
    {
        // cache
        private readonly IStore<TModel> _store;
        // database
        private readonly IStorage<TModel> _storage;

        public DomainManager(IStore<TModel> store, IStorage<TModel> storage)
        {
            _storage = storage;
            _store = store;
        }

        public async Task<TModel> AddAsync(TModel model)
        {
            var entity = await _storage.AddAsync(model);
            // set in store
            _store.Set(entity.Id, entity);

            return entity;
        }

        public async Task<TModel> FindByIdAsync(ulong id)
        {
            // search in store first
            var cached = _store.FindByIdOrDefault(id);

            if (cached != null) return cached;

            return await _storage.FindByIdAsync(id);
        }

        public async Task<ICollection<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression = null)
        {
            return await _storage.GetAllAsync(expression);
        }

        public async Task<TModel> UpdateAsync(TModel model)
        {
            if (model.State == RootState.Archived) _store.Remove(model.Id);

            return await _storage.UpdateAsync(model);
        }

        public async Task ReloadStoreAsync()
        {
            _store.Clear();

            // get only active
            var dtos = await _storage.GetAllAsync(t => t.State == RootState.Active);

            foreach (var dto in dtos) _store.Set(dto.Id, dto);
        }
    }
}
