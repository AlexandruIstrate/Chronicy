using Chronicy.Information;
using System;

namespace Chronicy.Utils
{
    public static class ExceptionUtils
    {
        public static void LogExceptions(Action action, IInformationContext informationContext)
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
