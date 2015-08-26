using System;
using System.Collections.Generic;
using HomeSecurity.DataTransferObjects;

namespace HomeSecurity.Interfaces.Services
{
    public interface ICameraService : IDisposable
    {
        /// <summary>
        /// Starts recording camera.
        /// </summary>
        void StartRecording(string monikerString);

        /// <summary>
        /// Stops recording camera.
        /// </summary>
        /// <returns></returns>
        void StopRecording(string monikerString);

        /// <summary>
        /// Gets or sets the connected cameras.
        /// </summary>
        /// <value>
        /// The connected cameras.
        /// </value>
        List<CameraDevice> ConnectedCameras { get; }
    }
}