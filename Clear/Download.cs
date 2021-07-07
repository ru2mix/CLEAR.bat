using FluentFTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;





namespace Clear
{
            
    public partial class Download : Form
    {

        public Download()
        {
            ///---- Запуск
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            string[] lineOfContents = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                comboBox1.Items.Add(tokens[0]);
            }
            comboBox1.SelectedItem = "7.7.6020.0";
            comboBox2.SelectedItem = "iikoFront";
        }
        ///---- Начало получения версий
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            Utilities.UpdateVersioniiko();
            // Обновление версий в программе
            string[] arr = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt"); File.WriteAllText(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt", string.Join(Environment.NewLine, arr.OrderBy(x => x)));
            string[] lineOfContents = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                comboBox1.Items.Add(tokens[0]);
            }
        }
        ///---- Конец получения версий
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        CancellationTokenSource cancel = null;
        // ASYNC
        public async Task DownloadFileAsync( string name, string iver, string setup)
        {
            if (cancel != null)
            {
                cancel.Cancel();
                cancel.Dispose();
                cancel = null;
                return;
            }
            cancel = new CancellationTokenSource();

            var token = new CancellationToken();
            using (var ftp = new FtpClient("ftp.iiko.ru", "partners", "partners#iiko"))
            {
                await ftp.ConnectAsync(token);
                long fileSize = await ftp.GetFileSizeAsync(name);
                fileSize /= 1024;

                Progress<FtpProgress> progress = new Progress<FtpProgress>(async loadedFile =>
                {
                    if (loadedFile.Progress == 100)
                    {
                        label5.Visible = false;
                        button3.Visible = false;
                        cancel.Dispose();
                        cancel = null;
                    }
                    else
                    {
                        int x = (int)fileSize * Convert.ToInt32(loadedFile.Progress);
                        string value = loadedFile.TransferSpeedToString();
                        label5.Visible = true;
                        button3.Visible = true;
                        label5.Text = "Скорость скачки: \n" + value;
                    }
                });
                try
                {
                    await ftp.DownloadFileAsync(@"C:\iiko_Distr\" + name, @"/release_iiko/" + iver + setup, FtpLocalExists.Append, FtpVerify.Retry, progress, cancel.Token);
            }
        catch
            {

            }
        }
        }
        //ASYNC


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        ///------- Начало скрипта скачки
        private async void button2_Click(object sender, EventArgs e)
        {

            try
            {
                //
                
                string iver = comboBox1.Text.ToString();
                string iexe = comboBox2.Text.ToString();
                if (iexe.Contains("iikoFront"))
                {
                    string setup = "/Setup/Offline/Setup.Front.exe";
                    string name = iver + "Front.exe";
                    // Start
                    await  DownloadFileAsync(name, iver, setup).ConfigureAwait(false);
                    // End
                    if (checkBox1.Checked == true)
                    {
                        string dir = @"C:\iiko_Distr\" + name;
                        Utilities.EXERunSetup(dir);
                    }
                }
                        if (iexe.Contains("iikoBackOffice"))
                {
                    string setup = "/Setup/Offline/Setup.RMS.BackOffice.exe";
                    string name = iver + "RMS_BackOffice.exe";
                    // Start
                    await DownloadFileAsync(name, iver, setup).ConfigureAwait(false);
                    // End
                    if (checkBox1.Checked == true)
                    {
                        string dir = @"C:\iiko_Distr\" + name;
                        Utilities.EXERunSetup(dir);
                    }
                }
                if (iexe.Contains("iikoChainOffice"))
                {
                    string setup = "/Setup/Offline/Setup.Chain.BackOffice.exe";
                    string name = iver + "Chain_BackOffice.exe";
                    // Start
                    await DownloadFileAsync(name, iver, setup).ConfigureAwait(false);
                    // End
                    if (checkBox1.Checked == true)
                    {
                        string dir = @"C:\iiko_Distr\" + name;
                        Utilities.EXERunSetup(dir);
                    }
                }
                if (iexe.Contains("Server_iikoRMS"))
                {
                    // ---------------CONFIG------------------------
                    {
                        label5.Visible = true;
                        label5.Text = "Скачивается";
                        string setup = "/Setup/RMS/";
                        NetworkCredential credentials = new NetworkCredential("partners", "partners#iiko");
                        string url = "ftp://ftp.iiko.ru/release_iiko/" + iver + setup;
                        string path = @"C:\iiko_Distr\RMS";
                        DownloadFtpDirectory(url, credentials, path + iver);
                        label5.Text = "Скачалось";
                        label5.Visible = false;
                        string exe = path + iver + "/Setup.RMS.exe";
                        ExeStart(exe);
                    }
                    // ----------------SCRIPT-----------------------
                    void DownloadFtpDirectory(string url, NetworkCredential credentials, string localPath)
                    {
                        FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(url);
                        listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                        listRequest.Credentials = credentials;

                        List<string> lines = new List<string>();

                        using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
                        using (Stream listStream = listResponse.GetResponseStream())
                        using (StreamReader listReader = new StreamReader(listStream))
                        {
                            while (!listReader.EndOfStream)
                            {
                                lines.Add(listReader.ReadLine());
                            }
                        }

                        foreach (string line in lines)
                        {
                            string[] tokens =
                                line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                            string name = tokens[8];
                            string permissions = tokens[0];

                            string localFilePath = Path.Combine(localPath, name);
                            string fileUrl = url + name;

                            if (permissions[0] == 'd')
                            {
                                if (!Directory.Exists(localFilePath))
                                {
                                    Directory.CreateDirectory(localFilePath);
                                }

                                DownloadFtpDirectory(fileUrl + "/", credentials, localFilePath);

                            }
                            else
                            {
                                FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                                downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                                downloadRequest.Credentials = credentials;

                                using (FtpWebResponse downloadResponse =
                                          (FtpWebResponse)downloadRequest.GetResponse())
                                using (Stream sourceStream = downloadResponse.GetResponseStream())
                                using (Stream targetStream = File.Create(localFilePath))
                                {
                                    byte[] buffer = new byte[10240];
                                    int read;
                                    while ((read = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        targetStream.Write(buffer, 0, read);

                                    }

                                }

                            }


                        }



                    }

                    // ------------------END---------------------
                    void ExeStart(string exe)
                    {
                        if (checkBox1.Checked == true)
                        {
                            Utilities.EXERunSetup(exe);
                        }
                    }

                }
                if (iexe.Contains("Server_iikoChain"))
                {
                    // ---------------CONFIG------------------------
                    {
                        label5.Visible = true;
                        label5.Text = "Скачивается";
                        string setup = "/Setup/Chain/";
                        NetworkCredential credentials = new NetworkCredential("partners", "partners#iiko");
                        string url = "ftp://ftp.iiko.ru/release_iiko/" + iver + setup;
                        string path = @"C:\iiko_Distr\Chain";
                        DownloadFtpDirectory(url, credentials, path + iver);
                        label5.Text = "Скачалось";
                        label5.Visible = false;
                        string exe = path + iver + "/Setup.Chain.exe";
                        ExeStart(exe);
                    }
                    // ----------------SCRIPT-----------------------
                    void DownloadFtpDirectory(string url, NetworkCredential credentials, string localPath)
                    {
                        FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(url);
                        listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                        listRequest.Credentials = credentials;

                        List<string> lines = new List<string>();

                        using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
                        using (Stream listStream = listResponse.GetResponseStream())
                        using (StreamReader listReader = new StreamReader(listStream))
                        {
                            while (!listReader.EndOfStream)
                            {
                                lines.Add(listReader.ReadLine());
                            }
                        }

                        foreach (string line in lines)
                        {
                            string[] tokens =
                                line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                            string name = tokens[8];
                            string permissions = tokens[0];

                            string localFilePath = Path.Combine(localPath, name);
                            string fileUrl = url + name;

                            if (permissions[0] == 'd')
                            {
                                if (!Directory.Exists(localFilePath))
                                {
                                    Directory.CreateDirectory(localFilePath);
                                }

                                DownloadFtpDirectory(fileUrl + "/", credentials, localFilePath);
                            }
                            else
                            {
                                FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                                downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                                downloadRequest.Credentials = credentials;

                                using (FtpWebResponse downloadResponse =
                                          (FtpWebResponse)downloadRequest.GetResponse())
                                using (Stream sourceStream = downloadResponse.GetResponseStream())
                                using (Stream targetStream = File.Create(localFilePath))
                                {
                                    byte[] buffer = new byte[10240];
                                    int read;
                                    while ((read = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        targetStream.Write(buffer, 0, read);
                                    }
                                }
                            }

                        }
                    }
                    void ExeStart(string exe)
                    {
                        if (checkBox1.Checked == true)
                        {
                            Utilities.EXERunSetup(exe);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("iikoDownloader.RunBatchFile", ex.Message, ex.StackTrace);
            }

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Download_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label5.Visible = false;
            button3.Visible = false;
            cancel.Dispose();
            cancel = null;
        }
    }
}
