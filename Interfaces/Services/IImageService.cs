using System.Drawing;

namespace HomeSecurity.Interfaces.Services
{
    public interface IImageService
    {
        /// <summary>
        /// Determines if person is in an image by facial recognition
        /// </summary>
        /// <param name="bitmap">Image to be searched.</param>
        /// <returns>True if face is detected</returns>
        bool ContainsPerson(Bitmap bitmap);

        /// <summary>
        /// Determines if person is in an image by facial recognition
        /// </summary>
        /// <param name="bitmap">Image to be searched.</param>
        /// <returns>Rectangle where face should be in.</returns>
        Rectangle[] ContainsPersonReturnMarker(Bitmap bitmap);
    }
}