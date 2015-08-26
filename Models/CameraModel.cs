using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge.Video.DirectShow;
using HomeSecurity.Interfaces.Models;

namespace HomeSecurityModels
{
    public class CameraModel : ICameraModel
    {
        private VideoCaptureDevice _videoCaptureDevice;
         
        public CameraModel(string friendlyName, string monikerString)
            :this (friendlyName, monikerString, new VideoCaptureDevice(monikerString))
        {
        }

        public CameraModel(string friendlyName, string monikerString, VideoCaptureDevice videoCaptureDevice)
        {
            _videoCaptureDevice = videoCaptureDevice;
            FriendlyName = friendlyName;
            MonikerString = monikerString;
            FrameSizes = _videoCaptureDevice?.VideoCapabilities.Select(s => s.FrameSize).ToList() ?? new List<Size>();
            DefaultSelectedFrameSize();
        }

        public List<Size> FrameSizes { get; private set; }

        public Size SelectedFrameSize { get; private set; }

        public string FriendlyName { get; private set; }
        
        public string MonikerString { get; private set; }
        
        public bool IsRunning { get; private set; }

        public void StartRecording()
        {
            if (IsRunning)
                return;
            
            try
            {
                IsRunning = true;
                _videoCaptureDevice.VideoResolution = _videoCaptureDevice.VideoCapabilities.First(f=>f.FrameSize == SelectedFrameSize);
                _videoCaptureDevice.Start();
                // TODO figure out frames should be passed to service etc
//                _videoCaptureDevice.NewFrame += NewFrameReceived;
//                _videoCaptureDevice.VideoSourceError += VideoError;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void StopRecording()
        {
            if (!IsRunning)
                return;

            try
            {
                IsRunning = false;
                _videoCaptureDevice.SignalToStop();
                _videoCaptureDevice.WaitForStop();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void SetFrameSize(Size frameSize)
        {
            Size userSelectedSize = FrameSizes.FirstOrDefault(f => f.Height == frameSize.Height && f.Width == frameSize.Width);
            if (userSelectedSize.IsEmpty)
                DefaultSelectedFrameSize();
        }

        public void Dispose()
        {
            StopRecording();
            _videoCaptureDevice = null;
        }

        /// <summary>
        /// Defaults the size of the selected frame to the largest size.
        /// </summary>
        private void DefaultSelectedFrameSize()
        {
            Size largestSize = new Size();
            foreach (Size frameSize in FrameSizes)
            {
                if (largestSize.Height <= frameSize.Height
                    && largestSize.Width <= frameSize.Width)
                    largestSize = frameSize;
            }

            SelectedFrameSize = largestSize;
        }
    }
}