using System;

namespace Chronicy.Service.Dispatch
{
    public interface IDispatcher
    {
        void Submit(Action action);
        void Remove(Action action);

        void Start();
        void Stop();
    }
}
