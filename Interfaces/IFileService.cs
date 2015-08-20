using System.Drawing;
using System.Threading.Tasks;

namespace HomeSecurity.Interfaces
{
    /// <summary>
    /// Interface for handling the uploading/backing up of files
    /// </summary>
    public interface IFileService
    {
        Task UplodateImageAsync(Bitmap image);
    }
}