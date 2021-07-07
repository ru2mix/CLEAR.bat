using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentFTP;

namespace Clear
{

	internal static class DownloadFileWithProgressExample
	{

		public static void DownloadFile(string iver)
		{
            string setup = "/Setup/Offline/Setup.Front.exe";
            string name = iver + "Front.exe";
            using (var ftp = new FtpClient("ftp.iiko.ru", "partners", "partners#iiko"))

            {
                ftp.Connect();

                // define the progress tracking callback
                Action<FtpProgress> progress = delegate (FtpProgress p) {
                    if (p.Progress == 1)
                    {

                    }
                    else
                    {


                    }
                };

                // download a file with progress tracking
                ftp.DownloadFile(@"C:\iiko_Distr\" + name, @"/release_iiko/" + iver + setup, FtpLocalExists.Overwrite, FtpVerify.None, progress);
            }
        }

        public static async Task DownloadFileAsync(string iver)
        {

            string setup = "/Setup/Offline/Setup.Front.exe";
            string name = iver + "Front.exe";
            var token = new CancellationToken();
            using (var ftp = new FtpClient("ftp.iiko.ru", "partners", "partners#iiko"))
            {
                await ftp.ConnectAsync(token);

                // define the progress tracking callback
                Progress<FtpProgress> progress = new Progress<FtpProgress>(p => {
                    if (p.Progress == 1)
                    {
                        // all done!
                    }
                    else
                    {
                        // percent done = (p.Progress * 100)

                    }
                });

                // download a file and ensure the local directory is created
                await ftp.DownloadFileAsync(@"C:\iiko_Distr\" + name, @"/release_iiko/" + iver + setup, FtpLocalExists.Append, FtpVerify.None, progress, token);

            }
        }




    }
}