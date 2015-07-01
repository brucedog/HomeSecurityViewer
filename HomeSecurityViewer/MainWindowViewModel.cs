using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Caliburn.Micro;
using DataTransferObjects;
using Interfaces;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

namespace HomeSecurityViewer
{
    public sealed class MainWindowViewModel : Screen, IHandle<SecurityImageEventMessage>, IHandle<Bitmap>, IHandle<string>
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

        public ImageSource SecurityImage
        {
            get { return _securityImage; }
            set
            {
                _securityImage = value;
                NotifyOfPropertyChange();
            }
        }

        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                NotifyOfPropertyChange();
            }
        }

        public void StartRecording()
        {
            if (SelectedCameraDevice == null)
                MessageBox.Show("Please select camera device first");

            _cameraService.SelectedCamera = SelectedCameraDevice;
            _cameraService.StartRecording();
        }

        public void StopRecording()
        {
            if (SelectedCameraDevice == null)
                MessageBox.Show("Please select camera device first");

            _cameraService.SelectedCamera = SelectedCameraDevice;
            _cameraService.StopRecording();
        }

        public void Handle(Bitmap image)
        {
            Application.Current.Dispatcher.Invoke(new ThreadStart(delegate
            {
                BitmapImage bi = new BitmapImage();
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Bmp);
                    ms.Position = 0;
                    bi.BeginInit();
                    bi.StreamSource = ms;

                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();

                    bi.Freeze();
                }            

                Image = bi;
            }));
        }

        public void Handle(string image)
        {
            MessageBox.Show(image);
        }

        public void Handle(SecurityImageEventMessage message)
        {
            Application.Current.Dispatcher.Invoke(new ThreadStart(delegate
            {
                Graphics graphics = Graphics.FromImage(message.ImageSource);
                foreach (Rectangle rectangle in message.OutlineRectangles)
                {
                    Pen pen = new Pen(Color.Crimson, 1.0f);
                    graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                }
               
                BitmapImage bi = new BitmapImage();
                using (MemoryStream ms = new MemoryStream())
                {
                    message.ImageSource.Save(ms, ImageFormat.Bmp);
                    ms.Position = 0;
                    bi.BeginInit();
                    bi.StreamSource = ms;

                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();

                    bi.Freeze();
                }
                graphics.Dispose();
                SecurityImage = bi;
            }));
        }
    }
}
