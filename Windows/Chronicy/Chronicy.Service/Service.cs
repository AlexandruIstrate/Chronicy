using Chronicy.Service.Information;
using Chronicy.Service.System;
using Chronicy.Utils;
using System.ServiceProcess;

namespace Chronicy.Service
{
    public partial class Service : ServiceBase
    {
        private EventLogContext context;
        private IService service;

        public Service()
        {
            context = new EventLogContext();
            service = new AppService();

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServiceStatus status = Status.StartPending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            ExceptionUtils.HandleExceptions(() => service.OnStart(), context);

            status = Status.Running;
            Status.SetServiceStatus(ServiceHandle, ref status);
        }

        protected override void OnPause()
        {
            ServiceStatus status = Status.PausePending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            ExceptionUtils.HandleExceptions(() => service.OnPause(), context);

            status = Status.Paused;
            Status.SetServiceStatus(ServiceHandle, ref status);
        }

        protected override void OnContinue()
        {
            ServiceStatus status = Status.ContinuePending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            ExceptionUtils.HandleExceptions(() => service.OnContinue(), context);
        }

        protected override void OnStop()
        {
            ServiceStatus status = Status.StopPending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            ExceptionUtils.HandleExceptions(() => service.OnStop(), context);

            status = Status.Stopped;
            Status.SetServiceStatus(ServiceHandle, ref status);
        }
    }
}
