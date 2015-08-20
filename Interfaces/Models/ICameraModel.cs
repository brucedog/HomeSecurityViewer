using System;

namespace HomeSecurity.Interfaces.Models
{
    public interface ICameraModel: IDisposable
    {
        string FriendlyName { get; }

        string MonikerString { get; }

        bool IsRunning { get; }

        void StartRecording();

        void StopRecording();
    }
}