using HomeSecurity.Interfaces;
using HomeSecurity.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServicesUnitTests
{
    [TestClass]
    public class SmokeDetectorServiceUnitTests
    {
        [TestMethod]
        public void SmokeDetectorServiceBasicConstruction()
        {
            ISmokeDetectorService smokeDetectorService = new SmokeDetectorService();

            Assert.IsNotNull(smokeDetectorService);
            Assert.IsTrue(smokeDetectorService.PollingInterval >= 100);
        }

        [TestMethod]
        public void SmokeDetectorServicePollingCannotbBeLessThanOneHunderd()
        {
            ISmokeDetectorService smokeDetectorService = new SmokeDetectorService();
            
            smokeDetectorService.PollingInterval = 1;
            
            Assert.IsTrue(smokeDetectorService.PollingInterval >= 100);
        }

        [TestMethod]
        public void SmokeDetectorServicePollingCanbBeGreaterThanOneHunderd()
        {
            ISmokeDetectorService smokeDetectorService = new SmokeDetectorService();

            smokeDetectorService.PollingInterval = 1000;

            Assert.IsTrue(smokeDetectorService.PollingInterval >= 1000);
        }
    }
}