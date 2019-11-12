using Chronicy.Service.Information;
using Chronicy.Service.App;
using System.ServiceProcess;
using System;
using Chronicy.Information;

namespace Chronicy.Service
{
    public partial class Service : ServiceBase
    {
        private readonly EventLogContext context;
        private readonly IService service;

        public Service()
        {
            context = EventLogContext.Current;
            service = new AppService();

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ServiceStatus status = Status.StartPending;
                Status.SetServiceStatus(ServiceHandle, ref status);

                service.OnStart();

                status = Status.Running;
                Status.SetServiceStatus(ServiceHandle, ref status);
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e, context);
            }
        }

        protected override void OnPause()
        {
            try
            {
                ServiceStatus status = Status.PausePending;
                Status.SetServiceStatus(ServiceHandle, ref status);

                service.OnPause();

                status = Status.Paused;
                Status.SetServiceStatus(ServiceHandle, ref status);
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e, context);
            }
        }

        protected override void OnContinue()
        {
            try
            {
                ServiceStatus status = Status.ContinuePending;
                Status.SetServiceStatus(ServiceHandle, ref status);

                service.OnContinue();
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e, context);
            }
        }

        protected override void OnStop()
        {
            try
            {
                ServiceStatus status = Status.StopPending;
                Status.SetServiceStatus(ServiceHandle, ref status);

                service.OnStop();

                status = Status.Stopped;
                Status.SetServiceStatus(ServiceHandle, ref status);
            }
            catch (Exception e)
            {
                InformationDispatcher.Default.Dispatch(e, context);
            }
        }
    }
}
