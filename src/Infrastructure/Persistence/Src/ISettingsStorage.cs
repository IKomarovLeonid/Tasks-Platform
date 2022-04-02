using System;
using System.Threading.Tasks;
using Environment.Events;
using Objects;

namespace Persistence.Storage
{
    public interface ISettingsStorage<TModel> where TModel : class, ISettings
    {
        Task<TModel> UpdateAsync(TModel model);
        
        Task<TModel> FindAsync(string key);

        IDisposable Subscribe(Action<StateEvent<TModel>> subscriber);
    }
}
