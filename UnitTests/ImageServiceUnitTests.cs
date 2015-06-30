using System.Drawing;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace UnitTests
{
    [TestClass]
    public class ImageServiceUnitTests
    {
        private Bitmap _emptyImage;
        private Bitmap _faceImage;

        [TestInitialize]
        public void Setup()
        {
            _emptyImage = new Bitmap(200, 200);
            _faceImage = new Bitmap(Properties.Resources.face);
        }

        [TestMethod]
        public void EmptyImageDoesNotContainPerson()
        {
            IImageService imageService = new ImageService();

            bool result = imageService.ContainsPerson(_emptyImage);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ImageOfPerspmDoesContainPerson()
        {
            IImageService imageService = new ImageService();

            bool result = imageService.ContainsPerson(_faceImage);

            Assert.IsTrue(result);
        }
    }
}
