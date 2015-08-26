using System;
using System.Collections.Generic;
using System.Drawing;

namespace HomeSecurity.Interfaces.Models
{
    public interface ICameraModel: IDisposable
    {
        List<Size> FrameSizes { get; }

        Size SelectedFrameSize { get; }
        
        string FriendlyName { get; }

        string MonikerString { get; }

        bool IsRunning { get; }

        void StartRecording();

        void StopRecording();

        void SetFrameSize(Size frameSize);
    }
}