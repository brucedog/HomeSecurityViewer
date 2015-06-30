using System.Drawing;

namespace Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// Determines if person is in an image by facial recognition
        /// </summary>
        /// <param name="bitmap">Image to be searched.</param>
        /// <returns>True if face is detected</returns>
        bool ContainsPerson(Bitmap bitmap);
    }
}