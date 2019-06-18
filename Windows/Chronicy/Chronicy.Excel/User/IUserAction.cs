using Chronicy.Information;

namespace Chronicy.Excel.User
{
    public abstract class IUserAction<T>
    {
        public delegate void ActionCompletedHandler(T data);
        public event ActionCompletedHandler ActionCompleted;

        public abstract void Run();

        protected void TriggerEvent(T data)
        {
            ActionCompleted?.Invoke(data);
        }
    }
}
