using Akka.Actor;
using log4net;
using Topshelf;

namespace CameraService
{
    class CameraService
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(CameraService));

        public bool Start()
        {
            _log.Info("Service is starting");
            ClusterSystem = ActorSystem.Create("camera");
            return true;
        }

        protected ActorSystem ClusterSystem { get; set; }

        public bool Stop()
        {
            _log.Info("Service is stopped");
            ClusterSystem.Shutdown();
            return true;
        }
    }
}
