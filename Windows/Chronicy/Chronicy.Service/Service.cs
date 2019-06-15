using Chronicy.Service.App;
using System;
using System.ServiceModel;
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
            service.OnStart();
        }

        protected override void OnStop()
        {
            service.OnStop();
        }
    }
}
