using Chronicy.Information;
using System;

namespace Chronicy.Utils
{
    public static class ExceptionUtils
    {
        public static void HandleExceptions(Action actionToTry, Action<Exception> onException)
        {
            try
            {
                actionToTry?.Invoke();
            }
            catch (Exception e)
            {
                onException?.Invoke(e);
            }
        }

        public static void HandleExceptions(Action action, IInformationContext informationContext)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e, informationContext);
            }
        }
    }
}
