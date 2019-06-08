using System;
using System.Diagnostics;

namespace Chronicy.Information
{
    public class LogInformationContext : IInformationContext
    {
        public void MessageDispatched(string message, InformationKind informationKind)
        {
            // TODO: Replace with NLog
            Debug.WriteLine($"[{ DateTime.Now }][{ informationKind }] { message }");
        }
    }
}
