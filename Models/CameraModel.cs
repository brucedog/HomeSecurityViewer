using System;
using AForge.Video.DirectShow;
using HomeSecurity.Interfaces.Models;

namespace HomeSecurityModels
{
    public class CameraModel : ICameraModel
    {
        private VideoCaptureDevice _videoCaptureDevice;
         
        public CameraModel(string friendlyName, string monikerString)
        {
            FriendlyName = friendlyName;
            MonikerString = monikerString;
            _videoCaptureDevice = new VideoCaptureDevice(MonikerString);
        }
        
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

        public void Dispose()
        {
            StopRecording();
            _videoCaptureDevice = null;
        }
    }
}