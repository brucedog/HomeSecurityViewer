using System;
using System.Collections.Generic;
using Accord;
using AForge.Video.DirectShow;
using DataTransferObjects;
using Interfaces;

namespace Services
{
    public class CameraService : ICameraService
    {
        private CameraDevice _selectedCamera;

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

                    // only add device if it has video capabilites
                    if (videoCaptureDevice.VideoCapabilities.Length > 0)
                    {                        
                        cameraDevices.Add(
                            new CameraDevice
                            {
                                Name = device.Name,
                                Description = device.GetDescription(),
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
            if (SelectedCamera == null)
                return false;

            try
            {
                VideoCaptureDevice videoCaptureDevice = new VideoCaptureDevice(SelectedCamera.MonikerString);
                // TODO add logic to set desired framesize and events for new frame
                videoCaptureDevice.Start();

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
            if (SelectedCamera == null)
                return false;

            try
            {
                VideoCaptureDevice videoCaptureDevice = new VideoCaptureDevice(SelectedCamera.MonikerString);
                videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop();

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
                VideoCaptureDevice videoCaptureDevice = new VideoCaptureDevice(SelectedCamera.MonikerString);
                if(!videoCaptureDevice.IsRunning)
                    _selectedCamera = value;
            }
        }
    }
}