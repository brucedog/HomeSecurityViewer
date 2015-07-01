using System;
using System.Drawing;
using Caliburn.Micro;
using Interfaces;

namespace Services
{
    public class SecurityService : ISecurityService, IHandle<Bitmap>
    {
        private ICameraService _cameraService;
        private IImageService _imageService;
        private IEventAggregator _eventAggregator;
        private IFileService _fileService;

        public SecurityService(IEventAggregator eventAggregator, 
            ICameraService cameraService, 
            IImageService imageService, 
            IFileService fileService)
        {
            _cameraService = cameraService;
            _imageService = imageService;
            _fileService = fileService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void Handle(Bitmap message)
        {
            try
            {
                if (_imageService.ContainsPerson(message))
                {
                    // TODO save some where.
                    UploadImage(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private async void UploadImage(Bitmap image)
        {
            await _fileService.UplodateImageAsync(image);
        }
    }
}