using Chronicy.Service.Information;
using Chronicy.Service.App;
using Chronicy.Utils;
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

                ExceptionUtils.LogExceptions(() => service.OnStart(), context);

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
            ServiceStatus status = Status.PausePending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            ExceptionUtils.LogExceptions(() => service.OnPause(), context);

            status = Status.Paused;
            Status.SetServiceStatus(ServiceHandle, ref status);
        }

        protected override void OnContinue()
        {
            ServiceStatus status = Status.ContinuePending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            ExceptionUtils.LogExceptions(() => service.OnContinue(), context);
        }

        protected override void OnStop()
        {
            ServiceStatus status = Status.StopPending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            ExceptionUtils.LogExceptions(() => service.OnStop(), context);

            status = Status.Stopped;
            Status.SetServiceStatus(ServiceHandle, ref status);
        }
    }
}
