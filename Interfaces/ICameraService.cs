using System;
using System.Collections.Generic;
using DataTransferObjects;

namespace Interfaces
{
    public interface ICameraService : IDisposable
    {
        /// <summary>
        /// Gets the available camera devices.
        /// </summary>
        /// <returns>List of available camera devices</returns>
        IList<CameraDevice> GetAvailableDevices();

        /// <summary>
        /// Starts recording if a camera is selected.
        /// </summary>
        /// <returns>false if no camera is selected</returns>
        bool StartRecording();

        /// <summary>
        /// Stops recording the selected camera.
        /// </summary>
        /// <returns></returns>
        bool StopRecording();

        /// <summary>
        /// Gets or sets the selected camera.
        /// Selected camera cannot be change until the camera is stopped.
        /// </summary>
        /// <value>
        /// The selected camera.
        /// </value>
        CameraDevice SelectedCamera { get; set; }
    }
}