using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using HomeSecurity.DataTransferObjects;
using HomeSecurity.Interfaces;
using HomeSecurity.Interfaces.Services;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

namespace HomeSecurity.Viewer.ViewModels
{
    public sealed class CameraViewerViewModel : Screen, IHandle<Bitmap>, IHandle<SecurityImageEventMessage>
    {
        private string _cameraName;
        private ImageSource _image;
        private readonly ICameraService _cameraService;
        private readonly CameraDevice _cameraDevice;
        private BitmapImage securityImage;

        public CameraViewerViewModel(ICameraService cameraService, CameraDevice cameraDevice)
        {
            _cameraService = cameraService;
            _cameraDevice = cameraDevice;
            CameraName = _cameraDevice.Name;
        }

        public string CameraName
        {
            get { return _cameraName; }
            set
            {
                _cameraName = value;
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

        public BitmapImage SecurityImage
        {
            get { return securityImage; }
            set
            {
                securityImage = value;
                NotifyOfPropertyChange();
            }
        }


        public void StartRecording()
        {
            _cameraService.SelectedCamera = _cameraDevice;
            _cameraService.StartRecording();
        }

        public void StopRecording()
        {
            _cameraService.SelectedCamera = _cameraDevice;
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