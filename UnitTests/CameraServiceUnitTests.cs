using System.Collections.Generic;
using Caliburn.Micro;
using DataTransferObjects;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;

namespace UnitTests
{
    /// <summary>
    /// Summary description for CameraServiceUnitTests
    /// </summary>
    [TestClass]
    public class CameraServiceUnitTests
    {
        private IEventAggregator _eventAggregator;

        [TestInitialize]
        public void Setup()
        {
            _eventAggregator = new Mock<IEventAggregator>().Object;
        }

        [TestMethod]
        public void FindListOfAvailableCameras()
        {
            ICameraService cameraService = new CameraService(_eventAggregator);

            IList<CameraDevice> devices =  cameraService.GetAvailableDevices();

            Assert.IsNotNull(devices);
        }

        [TestMethod]
        public void StartRecording()
        {
            ICameraService cameraService = new CameraService(_eventAggregator);

            bool cameraRecording = cameraService.StartRecording();

            Assert.IsFalse(cameraRecording);
        }

        [TestMethod]
        public void StopRecording()
        {
            ICameraService cameraService = new CameraService(_eventAggregator);

            bool cameraRecording = cameraService.StopRecording();

            Assert.IsFalse(cameraRecording);
        }
    }
}
