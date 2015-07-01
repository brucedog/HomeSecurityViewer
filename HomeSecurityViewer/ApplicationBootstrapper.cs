using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using Interfaces;
using Services;

namespace HomeSecurityViewer
{
    public class ApplicationBootstrapper : BootstrapperBase
    {        
        private readonly SimpleContainer _container = new SimpleContainer();

        public ApplicationBootstrapper()
        {
            Initialize();
        }

        
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var instance = _container.GetInstance(serviceType, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        protected override void Configure()
        {
            // register services
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<ICameraService, CameraService>();
            _container.Singleton<IImageService, ImageService>();
            // register viewmodels
            _container.Singleton<MainWindowViewModel, MainWindowViewModel>();
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            ICameraService cameraService = _container.GetInstance(typeof(ICameraService), null) as ICameraService;
            cameraService.Dispose();
        }
    }
}