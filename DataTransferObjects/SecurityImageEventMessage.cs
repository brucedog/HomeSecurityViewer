using System.Drawing;

namespace HomeSecurity.DataTransferObjects
{
    /// <summary>
    /// Class is used to display rectangle around a persons face.
    /// </summary>
    public class SecurityImageEventMessage
    {
        public Bitmap ImageSource { get; set; }
        public Rectangle[] OutlineRectangles { get; set; }
    }
}