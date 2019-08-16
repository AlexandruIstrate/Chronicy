#if NET472

using Chronicy.Information;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Chronicy.Communication
{
    public class ErrorHandlingServiceBehaviorAttribute : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase dispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                if (!(dispatcherBase is ChannelDispatcher channelDispatcher))
                {
                    continue;
                }

                channelDispatcher.ErrorHandlers.Add(new ErrorHandler());
            }
        }
    }

    public class ErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            InformationDispatcher.Default.Dispatch(error);
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
            {
                return;
            }

            FaultException faultException = new FaultException(error.Message);
            MessageFault messageFault = faultException.CreateMessageFault();

            fault = Message.CreateMessage(version, messageFault, faultException.Action);
        }
    }
}

#endif