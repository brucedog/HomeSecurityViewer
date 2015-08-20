using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HomeSecurity.Interfaces;

namespace HomeSecurity.Services
{
    public class FtpFileService : IFileService
    {
        private string _username;
        private string _ftpusername;
        private string _ftppassword;
        private string _ftpurl = "ftp://domain.com/wwwroot/security/";

        public async Task UplodateImageAsync(Bitmap image)
        {
            try
            {
                FtpWebRequest ftpClient = (FtpWebRequest)FtpWebRequest.Create(_ftpurl +"/" + _username + "_" + DateTime.Now.Ticks + "bmp");
                ftpClient.Credentials = new NetworkCredential(_ftpusername, _ftppassword);
                ftpClient.Method = WebRequestMethods.Ftp.UploadFile;
                ftpClient.UseBinary = true;
                ftpClient.KeepAlive = true;

                using (Stream requestStream = ftpClient.GetRequestStream())
                {
                    image.Save(requestStream, ImageFormat.Bmp);
                }

                await ftpClient.GetRequestStreamAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}