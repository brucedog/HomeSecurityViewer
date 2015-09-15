using HomeSecurity.Interfaces;
using HomeSecurity.Interfaces.Services;
using HomeSecurity.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServicesUnitTests
{
    [TestClass]
    public class MotionDetectorServiceUnitTests
    {
        [TestMethod]
        public void MotionDetectorServiceBasicConstruction()
        {
            IMotionDetectorService motionDetectorService = new MotionDetectorService();

            Assert.IsNotNull(motionDetectorService);
            Assert.IsTrue(motionDetectorService.PollingInterval >= 100);
        }

        [TestMethod]
        public void MotionDetectorServicePollingCannotbBeLessThanOneHunderd()
        {
            IMotionDetectorService motionDetectorService = new MotionDetectorService();
            
            motionDetectorService.PollingInterval = 1;
            
            Assert.IsTrue(motionDetectorService.PollingInterval >= 100);
        }

        [TestMethod]
        public void MotionDetectorServicePollingCanbBeGreaterThanOneHunderd()
        {
            IMotionDetectorService motionDetectorService = new MotionDetectorService();

            motionDetectorService.PollingInterval = 1000;

            Assert.IsTrue(motionDetectorService.PollingInterval >= 1000);
        }
    }
}