using System.Collections.Generic;
using DataTransferObjects;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace UnitTests
{
    /// <summary>
    /// Summary description for CameraServiceUnitTests
    /// </summary>
    [TestClass]
    public class CameraServiceUnitTests
    {
        [TestMethod]
        public void FindListOfAvailableCameras()
        {
            ICameraService cameraService = new CameraService();

            IList<CameraDevice> devices =  cameraService.GetAvailableDevices();

            Assert.IsNotNull(devices);
        }

        [TestMethod]
        public void StartRecording()
        {
            ICameraService cameraService = new CameraService();

            bool cameraRecording = cameraService.StartRecording();

            Assert.IsFalse(cameraRecording);
        }

        [TestMethod]
        public void StopRecording()
        {
            ICameraService cameraService = new CameraService();

            bool cameraRecording = cameraService.StopRecording();

            Assert.IsFalse(cameraRecording);
        }
    }
}
