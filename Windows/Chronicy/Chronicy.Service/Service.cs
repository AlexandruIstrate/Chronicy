using Chronicy.Service.System;
using System.ServiceProcess;

namespace Chronicy.Service
{
    public partial class Service : ServiceBase
    {
        // TODO: Better initialization
        private IService service = new AppService();

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServiceStatus status = Status.StartPending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            service.OnStart();

            status = Status.Running;
            Status.SetServiceStatus(ServiceHandle, ref status);
        }

        protected override void OnPause()
        {
            ServiceStatus status = Status.PausePending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            service.OnPause();

            status = Status.Paused;
            Status.SetServiceStatus(ServiceHandle, ref status);
        }

        protected override void OnContinue()
        {
            ServiceStatus status = Status.ContinuePending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            service.OnContinue();
        }

        protected override void OnStop()
        {
            ServiceStatus status = Status.StopPending;
            Status.SetServiceStatus(ServiceHandle, ref status);

            service.OnStop();

            status = Status.Stopped;
            Status.SetServiceStatus(ServiceHandle, ref status);
        }
    }
}
