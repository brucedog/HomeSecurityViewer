using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using Caliburn.Micro;
using HomeSecurity.DataTransferObjects;
using HomeSecurity.Interfaces;
using HomeSecurity.Interfaces.Services;

namespace HomeSecurity.Services
{
    public class CameraService : ICameraService
    {
        private CameraDevice _selectedCamera;
        private VideoCaptureDevice _videoCaptureDevice;
        private readonly IEventAggregator _eventAggregator;

        public CameraService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public IList<CameraDevice> GetAvailableDevices()
        {
            List<CameraDevice> cameraDevices = new List<CameraDevice>();
            try
            {
                FilterInfoCollection filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (filterInfoCollection.Count == 0)
                    return cameraDevices;

                foreach (FilterInfo device in filterInfoCollection)
                {
                    VideoCaptureDevice videoCaptureDevice = new VideoCaptureDevice(device.MonikerString);

                    // only add device if it has video capabilities
                    if (videoCaptureDevice.VideoCapabilities.Length > 0)
                    {                        
                        cameraDevices.Add(
                            new CameraDevice
                            {
                                Name = device.Name,
                                MonikerString = device.MonikerString
                            });
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                
                return cameraDevices;
            }

            return cameraDevices;            
        }

        public bool StartRecording()
        {
            if (_videoCaptureDevice == null)
                return false;

            try
            {
                // TODO add logic to set desired framesize and events for new frame
                _videoCaptureDevice.Start();
                _videoCaptureDevice.NewFrame += NewFrameReceived;
                _videoCaptureDevice.VideoSourceError += VideoError;

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return false;
        }

        public bool StopRecording()
        {
            if (_videoCaptureDevice == null)
                return false;

            try
            {
                _videoCaptureDevice.SignalToStop();
                _videoCaptureDevice.WaitForStop();
                _videoCaptureDevice.NewFrame -= NewFrameReceived;
                _videoCaptureDevice.VideoSourceError -= VideoError;

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return false;
        }

        public CameraDevice SelectedCamera
        {
            get { return _selectedCamera; }
            set
            {
                if (value == null || value == _selectedCamera)
                    return;

                _videoCaptureDevice = new VideoCaptureDevice(value.MonikerString);
                if(!_videoCaptureDevice.IsRunning)
                    _selectedCamera = value;
            }
        }

        public void Dispose()
        {
            if (_videoCaptureDevice != null && _videoCaptureDevice.IsRunning)
            {
                StopRecording();
            }
        }

        private void NewFrameReceived(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap) eventArgs.Frame.Clone();

            _eventAggregator.PublishOnCurrentThread(image);
        }

        private void VideoError(object sender, VideoSourceErrorEventArgs eventargs)
        {
            _eventAggregator.PublishOnBackgroundThread("VideoDeviceError");
        }
    }
}