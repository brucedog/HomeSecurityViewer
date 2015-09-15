using System.IO;
using System.Xml.Schema;
using log4net.Config;
using Topshelf;

namespace CameraService
{
    public class Program
    {
        static void Main()
        {
//            XmlConfigurator.ConfigureAndWatch(new FileInfo(".\\log4net.config"));
//            var host = HostFactory.New(x =>
//            {                
//                x.Service<CameraService>(s =>
//                {
//                    s.ConstructUsing(name => new CameraService());
//                    s.WhenStarted(tc =>
//                    {
//                        XmlConfigurator.ConfigureAndWatch(
//                            new FileInfo(".\\log4net.config"));
//                        tc.Start();
//                    });
//                    s.WhenStopped(tc => tc.Stop());
//                });
//
//                x.RunAsLocalSystem();
//                x.SetDescription("SampleService Description");
//                x.SetDisplayName("SampleService");
//                x.SetServiceName("SampleService");
//            });
//
//            host.Run();

            HostFactory.Run(
                r =>
                {
                    r.Service<CameraService>(s =>                        //2
                    {
                        s.ConstructUsing(name => new CameraService());     //3
                        s.WhenStarted(
                            tc =>
                            {
                                var fileInfo = new FileInfo(".\\log4net.config");
                                XmlConfigurator.ConfigureAndWatch(fileInfo);
                                tc.Start();
                            });              //4
                        s.WhenStopped(tc => tc.Stop());               //5
                    });
                    r.RunAsLocalSystem();                            //6

                    r.SetDescription("Sample Topshelf Host");        //7
                    r.SetDisplayName("Stuff");                       //8
                    r.SetServiceName("Stuff");
                });
        }
    }
}