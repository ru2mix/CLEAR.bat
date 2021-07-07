using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;


namespace Clear
{
    public partial class PluginsDowloader : Form
    {

        public PluginsDowloader()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            string[] lineOfContents = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_versions.txt");
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                comboBox1.Items.Add(tokens[0]);
                comboBox1.SelectedItem = "7.7.6020.0";
            }
            string[] lineOfPlugins = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt");
            foreach (var line in lineOfPlugins)
            {
                string[] tokens = line.Split(',');
                comboBox2.Items.Add(tokens[0]);
                comboBox2.SelectedItem = "Plugin.Front.Sbrf";
            }
            checkedListBox1.Items.Clear();
            var filesWithoutExtension = Directory.GetDirectories(@"C:/Program Files/iiko/iikoRMS/Front.Net/Plugins/");
            foreach (string folder in filesWithoutExtension)
            {
                checkedListBox1.Items.Add(Path.GetFileName(folder));
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void copyright_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string iver = comboBox1.Text.ToString();
            Utilities.UpdatePluginsList(iver);
            string[] arr = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt"); File.WriteAllText(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt", string.Join(Environment.NewLine, arr.OrderBy(x => x)));
            string[] lineOfContents = File.ReadAllLines(Helper.ProgramData + @"\CLEAR_bat\bat\iiko_plugins.txt");
            foreach (var line in lineOfContents)
            {

                string[] tokens = line.Split(',');
                comboBox2.Items.Add(tokens[0]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            textBox1.Visible = true;
            // ---------------CONFIG------------------------
            {
                string iver = comboBox1.Text.ToString();
                string pluginname = comboBox2.Text.ToString();
                NetworkCredential credentials = new NetworkCredential("partners", "partners#iiko");
                string url = "ftp://ftp.iiko.ru/release_iiko/" + iver + "/Plugins/Front/" + pluginname + "/";

                string path = @"C:/Program Files/iiko/iikoRMS/Front.Net/Plugins/" + pluginname + "/";
                Directory.CreateDirectory(path);
                DownloadFtpDirectory(url, credentials, path);
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
            if (checkBox1.Checked)
            {
                Utilities.Corflags();
            }
            label4.Visible = false;
            textBox1.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // declaring & loading dom
            label4.Visible = true;
            textBox1.Visible = true;
            string URL = "http://rapid.iiko.ru/plugins/Smart%20Sberbank/V6/";
            string path = Helper.ProgramData + @"\CLEAR_bat\bat\iiko_sbrf.txt";
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(URL);

            // extracting all links
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];

                if (att.Value.Contains("a"))
                {
                    File.WriteAllText(path, att.Value);
                    Console.WriteLine(att.Value);
                }

            }
            label4.Visible = false;
            textBox1.Visible = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            textBox1.Visible = true;
            // Скачиваю плагин Сбербанк
            string URL = "http://rapid.iiko.ru/plugins/Smart%20Sberbank/V6/";
            string dirname = @"Resto.Front.Api.SberbankPlugin\";
            string name = "Resto.Front.Api.SberbankPlugin.zip";
            string iikoPath = @"C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\";
            string plugins = @"C:/iiko_Distr/plugins/";
            string zipPath = plugins + name;
            string extractPath = iikoPath + dirname;
            Directory.CreateDirectory(iikoPath + dirname);
            Directory.CreateDirectory(Helper.ProfileAppDataRoaming + @"\iiko\CashServer\PluginConfigs\Resto.Front.Api.SberbankPlugin");
            Directory.CreateDirectory(@"C:/iiko_Distr/plugins/");
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load("http://rapid.iiko.ru/plugins/Smart%20Sberbank/V6/");

            // extracting all links
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];

                if (att.Value.Contains(".zip"))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(URL + att.Value, plugins + name);
                    }
                }
            }
            {

                if (Directory.Exists(iikoPath + dirname))
                {
                    Directory.Delete(iikoPath + dirname, true);
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
            }
            {
                if (File.Exists(Helper.ProfileAppDataRoaming + @"\iiko\CashServer\PluginConfigs\Resto.Front.Api.SberbankPlugin\" + "Resto.Front.Api.SberbankPlugin.dll.config"))
                {
                    File.Delete(Helper.ProfileAppDataRoaming + @"\iiko\CashServer\PluginConfigs\Resto.Front.Api.SberbankPlugin\" + "Resto.Front.Api.SberbankPlugin.dll.config");
                }
                File.Copy(iikoPath + dirname + "Resto.Front.Api.SberbankPlugin.dll.config", Helper.ProfileAppDataRoaming + @"\iiko\CashServer\PluginConfigs\Resto.Front.Api.SberbankPlugin\Resto.Front.Api.SberbankPlugin.dll.config");
                Directory.Delete(plugins, true);
            }
            // Конец сбер
            label4.Visible = false;
            textBox1.Visible = false;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            label4.Visible = true;
            textBox1.Visible = true;
            //Config
            string URL = "http://rapid.iiko.ru/plugins/Resto.Front.Api.DigitalSignage/V6/";
            string dirname = @"Resto.Front.Api.DigitalSignage\";
            string name = "Resto.Front.Api.DigitalSignage.zip";
            Utilities.PluginsRapidDownload(URL, dirname, name);
            label4.Visible = false;
            textBox1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            textBox1.Visible = true;
            //Config
            string URL = "http://rapid.iiko.ru/plugins/LoyaltyPlantPlugin/";
            string dirname = @"Resto.Front.Api.LoyaltyPlantPlugin\";
            string name = "Resto.Front.Api.LoyaltyPlantPlugin.zip";
            Utilities.PluginsRapidDownload(URL, dirname, name);
            label4.Visible = false;
            textBox1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            textBox1.Visible = true;
            string URL = "http://rapid.iiko.ru/plugins/PlaziusCheckin/V6/";
            string dirname = @"Resto.Front.Api.PlaziusCheckinPlugin\";
            string name = "Resto.Front.Api.PlaziusCheckinPlugin.zip";
            Utilities.PluginsRapidDownload(URL, dirname, name);
            label4.Visible = false;
            textBox1.Visible = false;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string iikoPath = @"C:\Program Files\iiko\iikoRMS\Front.Net\Plugins\";
                if (checkedListBox1.CheckedItems.Count != 0)
                {
                    foreach (string folder in checkedListBox1.CheckedItems)
                    {
                        string name = folder;
                        Directory.Delete(iikoPath + name, true);
                    }

                }
                checkedListBox1.Items.Clear();
                var filesWithoutExtension = Directory.GetDirectories(@"C:/Program Files/iiko/iikoRMS/Front.Net/Plugins/");
                foreach (string folder in filesWithoutExtension)
                {
                    checkedListBox1.Items.Add(Path.GetFileName(folder));
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("PluginsRemover.RunBatchFile", ex.Message, ex.StackTrace);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                checkedListBox1.Items.Clear();
                var filesWithoutExtension = Directory.GetDirectories(@"C:/Program Files/iiko/iikoRMS/Front.Net/Plugins/");
                foreach (string folder in filesWithoutExtension)
                {
                    checkedListBox1.Items.Add(Path.GetFileName(folder));
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("PluginsRemover.RunBatchFile", ex.Message, ex.StackTrace);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            textBox1.Visible = true;
            string URL = "https://disk.iiko.pro/plugins/iikoWaiter5/";
            string dirname = @"\";
            string name = "iikoWaiter5.zip";
            Utilities.PluginsRapidDownload(URL, dirname, name);
            label4.Visible = false;
            textBox1.Visible = false;
            Utilities.RunBatchFile(Required.ScriptsFolder + "iiko_ports.bat");
        }
    }
}