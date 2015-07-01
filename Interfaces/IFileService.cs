using System.Drawing;
using System.Threading.Tasks;

namespace Interfaces
{
    /// <summary>
    /// Interface for handling the uploading/backing up of files
    /// </summary>
    public interface IFileService
    {
        Task UplodateImageAsync(Bitmap image);
    }
}