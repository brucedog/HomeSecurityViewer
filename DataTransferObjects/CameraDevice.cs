using System.Collections.Generic;
using System.Drawing;

namespace HomeSecurity.DataTransferObjects
{
    public class CameraDevice
    {
        public string Name { get; set; }
        public string MonikerString { get; set; }
        public List<Size> AvailableFrameSizes { get; set; }
    }
}
