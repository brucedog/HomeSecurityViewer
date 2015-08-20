using HomeSecurity.Interfaces;
using HomeSecurity.Interfaces.Services;
using HomeSecurity.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServicesUnitTests
{
    [TestClass]
    public class CarbonMonoxideDetectorServiceUnitTests
    {
        [TestMethod]
        public void CarbonMonoxideDetectorServiceBasicConstruction()
        {
            ICarbonMonoxideDetectorService carbonMonoxideDetectorService = new CarbonMonoxideDetectorService();

            Assert.IsNotNull(carbonMonoxideDetectorService);
            Assert.IsTrue(carbonMonoxideDetectorService.PollingInterval >= 100);
        }

        [TestMethod]
        public void CarbonMonoxideDetectorServicePollingCannotbBeLessThanOneHunderd()
        {
            ICarbonMonoxideDetectorService carbonMonoxideDetectorService = new CarbonMonoxideDetectorService();
            
            carbonMonoxideDetectorService.PollingInterval = 1;
            
            Assert.IsTrue(carbonMonoxideDetectorService.PollingInterval >= 100);
        }

        [TestMethod]
        public void CarbonMonoxideDetectorServicePollingCanbBeGreaterThanOneHunderd()
        {
            ICarbonMonoxideDetectorService carbonMonoxideDetectorService = new CarbonMonoxideDetectorService();

            carbonMonoxideDetectorService.PollingInterval = 1000;

            Assert.IsTrue(carbonMonoxideDetectorService.PollingInterval >= 1000);
        }
    }
}