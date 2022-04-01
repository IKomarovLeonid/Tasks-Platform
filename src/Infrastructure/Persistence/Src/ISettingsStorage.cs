using System;
using System.Threading.Tasks;
using Objects;
using Persistence.Src.Events;

namespace Persistence.Storage
{
    public interface ISettingsStorage<TModel> where TModel : class, ISettings
    {
        Task<TModel> UpdateAsync(TModel model);
        
        Task<TModel> FindAsync(string key);

        IDisposable Subscribe(Action<StateEvent<TModel>> subscriber);
    }
}
