using System.Collections.Generic;
using System.Drawing;
using Caliburn.Micro;
using HomeSecurity.DataTransferObjects;
using HomeSecurity.Interfaces;
using HomeSecurity.Interfaces.Models;
using HomeSecurity.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HomeSecurity.Services;

namespace ServicesUnitTests
{
    /// <summary>
    /// Summary description for CameraServiceUnitTests
    /// </summary>
    [TestClass]
    public class CameraServiceUnitTests
    {
        private IEventAggregator _eventAggregator;
        private List<ICameraModel> _cameraModels;
        private List<CameraDevice> _cameraDevices;
        private Mock<ICameraModel> _mockCameraModel;

        [TestInitialize]
        public void Setup()
        {
            _eventAggregator = new Mock<IEventAggregator>().Object;
            _mockCameraModel = new Mock<ICameraModel>();
            _mockCameraModel.Setup(s => s.FriendlyName).Returns("camera1");
            _mockCameraModel.Setup(s => s.MonikerString).Returns("usb:camera1");
            _cameraModels = new List<ICameraModel> { _mockCameraModel.Object };
            _cameraDevices= new List<CameraDevice> { new CameraDevice {Name = "camera1", MonikerString = "usb:camera1", AvailableFrameSizes = new List<Size>()} };
        }

//        [TestMethod]
//        public void StartRecording()
//        {
//            _mockCameraModel.Setup(s=>s.StartRecording()).Verifiable();
//            ICameraService cameraService = new CameraService(_eventAggregator, _cameraDevices, _cameraModels);
//
//            cameraService.StartRecording("usb:camera1");
//            
//            _mockCameraModel.Verify();
//        }
//
//        [TestMethod]
//        public void StopRecording()
//        {
//            _mockCameraModel.Setup(s => s.StopRecording()).Verifiable();
//            ICameraService cameraService = new CameraService(_eventAggregator, _cameraDevices, _cameraModels);
//
//            cameraService.StopRecording("usb:camera1");
//
//            _mockCameraModel.Verify();
//        }
    }
}
