using System;
namespace Processing.Src
{
    public interface IListener : IDisposable
    {
        void Start();

        void Stop();
    }
}
