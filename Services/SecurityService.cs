using System;
using System.Drawing;
using Caliburn.Micro;
using DataTransferObjects;
using Interfaces;

namespace Services
{
    public class SecurityService : ISecurityService, IHandle<Bitmap>
    {
        private ICameraService _cameraService;
        private IImageService _imageService;
        private IEventAggregator _eventAggregator;

        public SecurityService(IEventAggregator eventAggregator, ICameraService cameraService, IImageService imageService)
        {
            _cameraService = cameraService;
            _imageService = imageService;
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
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}