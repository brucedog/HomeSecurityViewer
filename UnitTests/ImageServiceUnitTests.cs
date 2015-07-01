using System.Drawing;
using Caliburn.Micro;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;

namespace UnitTests
{
    [TestClass]
    public class ImageServiceUnitTests
    {
        private Bitmap _emptyImage;
        private Bitmap _faceImage;
        private IEventAggregator _eventAggregator;
        
        [TestInitialize]
        public void Setup()
        {
            _eventAggregator = new Mock<IEventAggregator>().Object;
            _emptyImage = new Bitmap(200, 200);
            _faceImage = new Bitmap(Properties.Resources.face);
        }

        [TestMethod]
        public void EmptyImageDoesNotContainPerson()
        {
            IImageService imageService = new ImageService(_eventAggregator);

            bool result = imageService.ContainsPerson(_emptyImage);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ImageOfPerspmDoesContainPerson()
        {
            IImageService imageService = new ImageService(_eventAggregator);

            bool result = imageService.ContainsPerson(_faceImage);

            Assert.IsTrue(result);
        }
    }
}
