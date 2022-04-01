using System;

namespace Processing
{
    public interface IListener : IDisposable
    {
        void Start();

        void Stop();
    }
}
