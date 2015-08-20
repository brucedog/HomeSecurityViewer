using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Caliburn.Micro;
using HomeSecurity.DataTransferObjects;
using HomeSecurity.Interfaces;

namespace HomeSecurity.Viewer.ViewModels
{
    public sealed class MainWindowViewModel : Screen, IHandle<string>
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ICameraService _cameraService;
        private ImageSource _image;
        private ImageSource _securityImage;
        private ISecurityService _securityService;

        public MainWindowViewModel(IWindowManager windowManager, IEventAggregator eventAggregator,
            ISecurityService securityService, ICameraService cameraService)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _cameraService = cameraService;
            _securityService = securityService;
            DisplayName = "Home Security Viewer";
            Initialize();
        }

        private async void Initialize()
        {
            await GetCameraDevices();
            _eventAggregator.Subscribe(this);
        }

        private Task GetCameraDevices()
        {
            return Task.Run(() =>
                            {
                                IList<CameraDevice> devices = _cameraService.GetAvailableDevices();
                                Dispatcher.CurrentDispatcher.Invoke(() =>
                                                                    {
                                                                        AvailableCameraDevices = new BindableCollection<CameraDevice>(devices);
                                                                    });
                            });
        }

        public IObservableCollection<CameraDevice> AvailableCameraDevices { get; set; }   

        public CameraDevice SelectedCameraDevice { get; set; }


        public void Handle(string image)
        {
            MessageBox.Show(image);
        }
    }
}
