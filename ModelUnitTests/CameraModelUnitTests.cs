using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Video.DirectShow;
using HomeSecurity.Interfaces.Models;
using HomeSecurityModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ModelUnitTests
{
    [TestClass]
    public class CameraModelUnitTests
    {
        private Mock<VideoCaptureDevice> _videoDevice;

        [TestInitialize]
        public void Setup()
        {
            _videoDevice = new Mock<VideoCaptureDevice>();            
        }

        [TestMethod]
        public void CameraModel_Constructs()
        {
            ICameraModel cameraModel = new CameraModel("camera1", "usb:/camera1", _videoDevice.Object);

            Assert.IsNotNull(cameraModel);
        }

        [TestMethod]
        public void CameraModel_SetFrameSize()
        {
            Size newFrameSize = new Size
            {
                Width = 480,
                Height = 680
            };
            ICameraModel cameraModel = new CameraModel("camera1", "usb:/camera1", _videoDevice.Object);

            cameraModel.SetFrameSize(newFrameSize);

            Assert.AreNotEqual(cameraModel.SelectedFrameSize, newFrameSize);
        }

        [TestMethod]
        public void CameraModel_NoVideoDevice()
        {
            ICameraModel cameraModel = new CameraModel("camera1", "usb:/camera1", null);
            
            Assert.IsNotNull(cameraModel);
        }

        [TestMethod]
        public void CameraModel_Invalid()
        {
            ICameraModel cameraModel = new CameraModel("camera1", "usb:/camera1");

            Assert.IsNotNull(cameraModel);
        }
    }
}
