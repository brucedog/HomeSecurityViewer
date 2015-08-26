using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge.Video;
using AForge.Video.DirectShow;
using Caliburn.Micro;
using HomeSecurity.DataTransferObjects;
using HomeSecurity.Interfaces.Models;
using HomeSecurity.Interfaces.Services;
using HomeSecurityModels;

namespace HomeSecurity.Services
{
    public class CameraService : ICameraService
    {
        private List<ICameraModel> _cameraModels = new List<ICameraModel>();
        private readonly IEventAggregator _eventAggregator;
        private readonly List<CameraDevice> cameraDevices = new List<CameraDevice>();


//        #region constructor for unit testing
//        public CameraService(IEventAggregator eventAggregator, List<CameraDevice> cameraDevices, List<ICameraModel> cameraModels)
//        {
//            _eventAggregator = eventAggregator;
//            cameraDevices.AddRange(cameraDevices);
//            _cameraModels.AddRange(cameraModels);
//        }
//        #endregion

        public CameraService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            GetAvailableDevices();
        }

        private void GetAvailableDevices()
        {
            try
            {
                FilterInfoCollection filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (filterInfoCollection.Count == 0)
                    return;

                foreach (FilterInfo device in filterInfoCollection)
                {
                    VideoCaptureDevice videoCaptureDevice = new VideoCaptureDevice(device.MonikerString);

                    // only add device if it has video capabilities
                    if (videoCaptureDevice.VideoCapabilities.Length > 0)
                    {
                        ICameraModel cameraModel = new CameraModel(device.Name, device.MonikerString);
                        _cameraModels.Add(cameraModel);

                        cameraDevices.Add(
                            new CameraDevice
                            {
                                Name = device.Name,
                                MonikerString = device.MonikerString,
                                AvailableFrameSizes = cameraModel.FrameSizes
                            });
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }          
        }

        public void StartRecording(string monikerString)
        {
            ICameraModel camera = _cameraModels.FirstOrDefault(f => f.MonikerString.Equals(monikerString));

            camera?.StartRecording();
        }

        public void StopRecording(string monikerString)
        {
            ICameraModel camera = _cameraModels.FirstOrDefault(f => f.MonikerString.Equals(monikerString));

            camera?.StopRecording();
        }

        public List<CameraDevice> ConnectedCameras => cameraDevices;

        public void Dispose()
        {
            foreach (ICameraModel cameraModel in _cameraModels)
            {
                cameraModel.StopRecording();
            }
            _cameraModels = null;
        }
    }
}