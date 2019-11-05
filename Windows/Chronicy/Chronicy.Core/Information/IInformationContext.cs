using System;

namespace Chronicy.Information
{
    /// <summary>
    /// Provides a way of communicating data in a communication-capable context.
    /// </summary>
    public interface IInformationContext
    {
        void MessageDispatched(string message, InformationKind informationKind);
        void ExceptionDispatched(Exception exception);
    }
}
