using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace Clear
{
    public partial class Home : Form
    {

        public Home()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            //UPDATE
            Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            WebClient http = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            Version latestVersion = new Version(http.DownloadString("https://github.com/ru2mix/CLEAR.bat/raw/main/version.txt"));
            if (latestVersion > currentVersion)
            {
                DownloadUpdate upd = new DownloadUpdate();
                this.Hide();
                upd.textBox1.Text = "Доступно обновление, версия: " + latestVersion + Environment.NewLine + "Скачать?";
                upd.ShowDialog();
                upd.Close();
                this.Show();
            }
            //START        
            label5.Text = "v. " + Application.ProductVersion.Substring(0, 3) + Helper.VerInfo;
            textBox3.Text = "443";
            comboBox1.SelectedItem = "4.7.2";
            button25Clicked = false;
            button26Clicked = false;

        }
        private bool button25Clicked = false;
        private bool button26Clicked = false;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void full_clear_Click(object sender, EventArgs e)
        {

        }

        private void win_button_Click(object sender, EventArgs e)
        {

        }

        private void cl_wo_pl_Click(object sender, EventArgs e)
        {

        }

        private void dowload_Click(object sender, EventArgs e)
        {

        }

        private void update_Click(object sender, EventArgs e)
        {
            Cofee upd = new Cofee();
            this.Hide();
            upd.ShowDialog();
            Application.Exit();
        }

        private void copyright_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
                Start win = new Start();
                win.Show();
                Hide();
                Utilities.RunBatchFile(Required.ScriptsFolder + "clean_all.bat");
                win.Close();   
                this.Show();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void front_button_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "del_plazius.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/rlato");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Info win = new Info();
            win.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "clean_wo_pl.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void full_clear_Click_1(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "clear_front.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void win_button_Click_1(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "del_plazius.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void cl_wo_pl_Click_1(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "clear_iikocard.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void dowload_Click_1(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "iiko_ports.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Utilities.Corflags();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            PluginsDowloader pl = new PluginsDowloader();
            this.Hide();
            pl.ShowDialog();
            this.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "start_front.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "clear_system.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "stop_service.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "time_sync.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Start win = new Start();
            win.Show();
            Utilities.RunBatchFile(Required.ScriptsFolder + "qr_win_upd.bat");
            win.Close();
            this.Show();
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Download dwn = new Download();
            dwn.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string net = comboBox1.Text.ToString();
            if (net.Contains("4.8"))
            {
                label9.Visible = true;
                textBox4.Visible = true;
                string url = "https://go.microsoft.com/fwlink/?linkid=2088631";
                string dir = @"C:/iiko_Distr/Net48.exe";
                Utilities.HTTPDownload(url, dir);
                label9.Visible = false;
                textBox4.Visible = false;
            }
            if (net.Contains("4.7.2"))
            {
                label9.Visible = true;
                textBox4.Visible = true;
                string url = "http://go.microsoft.com/fwlink/?LinkId=863265";
                string dir = @"C:/iiko_Distr/Net472.exe";
                Utilities.HTTPDownload(url, dir);
                label9.Visible = false;
                textBox4.Visible = false;
            }
            if (net.Contains("4.5.2"))
            {
                label9.Visible = true;
                textBox4.Visible = true;
                string url = "http://download.microsoft.com/download/E/2/1/E21644B5-2DF2-47C2-91BD-63C560427900/NDP452-KB2901907-x86-x64-AllOS-ENU.exe";
                string dir = @"C:/iiko_Distr/Net452.exe";
                Utilities.HTTPDownload(url, dir);
                label9.Visible = false;
                textBox4.Visible = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Utilities.RunBatchFile(Required.ScriptsFolder + "folder_iikofrontlogs.bat");
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Utilities.RunBatchFile(Required.ScriptsFolder + "folder_iikofront.bat");
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            textBox4.Visible = true;
            Utilities.RunBatchFile(Required.ScriptsFolder + "folder_distr.bat");
            label9.Visible = false;
            textBox4.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {

            Utilities.RunBatchFile(Required.ScriptsFolder + "rmdir.bat");
            this.Close();

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            Cofee upd = new Cofee();
            this.Hide();
            upd.ShowDialog();
            Application.Exit();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Download dwn = new Download();
            this.Hide();
            dwn.ShowDialog();
            this.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    string url = textBox1.Text;
                    string port = textBox3.Text;
                    string http = "HTTP";

                    XmlDocument doc1 = new XmlDocument();
                    doc1.Load("http://" + url + ":" + port + "/resto/get_server_info.jsp?encoding=UTF-8");
                    XmlElement root = doc1.DocumentElement;
                    XmlNodeList nodes = root.SelectNodes("/r");

                    foreach (XmlNode node in nodes)
                    {
                        textBox2.Clear();
                        string serverName = node["serverName"].InnerText;
                        string version = node["version"].InnerText;
                        string serverState = node["serverState"].InnerText;
                        string edition = node["edition"].InnerText;
                        textBox2.Visible = true;
                        textBox2.AppendText("Имя сервера: " + serverName + Environment.NewLine);
                        textBox2.AppendText("Версия iiko: " + version + Environment.NewLine);
                        textBox2.AppendText("Статус сервера: " + serverState + Environment.NewLine);
                        textBox2.AppendText("Тип: " + edition + Environment.NewLine);
                        button17.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError("Getiikoversionxml.RunBatchFile", ex.Message, ex.StackTrace);
                }
            }
            if (checkBox1.Checked == false)
            {
                try
                {
                    string url = textBox1.Text;
                    string port = "443";
                    string http = "HTTPS";

                    XmlDocument doc1 = new XmlDocument();
                    doc1.Load("https://" + url + "/resto/get_server_info.jsp?encoding=UTF-8");
                    XmlElement root = doc1.DocumentElement;
                    XmlNodeList nodes = root.SelectNodes("/r");

                    foreach (XmlNode node in nodes)
                    {
                        textBox2.Clear();
                        string serverName = node["serverName"].InnerText;
                        string version = node["version"].InnerText;
                        string serverState = node["serverState"].InnerText;
                        string edition = node["edition"].InnerText;
                        textBox2.Visible = true;
                        textBox2.AppendText("Имя сервера: " + serverName + Environment.NewLine);
                        textBox2.AppendText("Версия iiko: " + version + Environment.NewLine);
                        textBox2.AppendText("Статус сервера: " + serverState + Environment.NewLine);
                        textBox2.AppendText("Тип: " + edition + Environment.NewLine);
                        button17.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError("Getiikoversionxml.RunBatchFile", ex.Message, ex.StackTrace);
                }
            }
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            textBox2.Clear();
            button17.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox3.Clear();
                textBox3.Visible = true;
                label8.Visible = true;
            }
            else
            {
                textBox3.Text = "443";
                textBox3.Visible = false;
                label8.Visible = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            Utilities.RunBatchFile(Required.ScriptsFolder + "folder_iikocard.bat");

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void tab_download_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            Cofee cofee = new Cofee();
            this.Hide();
            cofee.ShowDialog();
            this.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            // Открывашка бека
            if (checkBox1.Checked == true)
            {
                try
                {
                    string url = textBox1.Text;
                    string port = textBox3.Text;
                    string http = "http";
                    string path = File.ReadAllText(Helper.ProgramData + @"\CLEAR_bat\bin\BackPath.txt");
                    XmlDocument doc1 = new XmlDocument();
                    doc1.Load("http://" + url + ":" + port + "/resto/get_server_info.jsp?encoding=UTF-8");
                    XmlElement root = doc1.DocumentElement;
                    XmlNodeList nodes = root.SelectNodes("/r");
                    string edition = null;
                    string versionraw = null;
                    string ComputerName = null;
                    string serverName = null;
                    foreach (XmlNode node in nodes)
                    {
                        textBox2.Clear();
                        serverName = node["serverName"].InnerText;
                        versionraw = node["version"].InnerText;
                        string serverState = node["serverState"].InnerText;
                        edition = node["edition"].InnerText;
                        ComputerName = node["ComputerName"].InnerText;
                        textBox2.Visible = true;
                        textBox2.AppendText("Имя сервера: " + serverName + Environment.NewLine);
                        textBox2.AppendText("Версия iiko: " + versionraw + Environment.NewLine);
                        textBox2.AppendText("Статус сервера: " + serverState + Environment.NewLine);
                        textBox2.AppendText("Тип: " + edition + Environment.NewLine);
                        button17.Visible = true;
                    }
                    string rms = null;
                    string folder = null;
                    if (edition.Contains("default"))
                    {
                        rms = "RMS";
                        folder = "Office";
                    }
                    if (edition.Contains("chain"))
                    {
                        rms = "Chain";
                        folder = "Coffice";
                    }
                    string versiondot = versionraw.Replace(".", string.Empty);
                    string version = versiondot.Substring(0, 3);
                    string dir = path + @"/" + rms + @"/" + folder + version + @"/" + "BackOffice.exe";
                    Utilities.CofnigBackOffice(url, port, http, dir, edition, versionraw, ComputerName, serverName);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError("Getiikoversionxml.RunBatchFile", ex.Message, ex.StackTrace);
                }
            }
            if (checkBox1.Checked == false)
            {
                try
                {
                    string url = textBox1.Text;
                    string port = textBox3.Text;
                    string http = "https";
                    string path = File.ReadAllText(Helper.ProgramData + @"\CLEAR_bat\bin\BackPath.txt");
                    XmlDocument doc1 = new XmlDocument();
                    doc1.Load("https://" + url + "/resto/get_server_info.jsp?encoding=UTF-8");
                    XmlElement root = doc1.DocumentElement;
                    XmlNodeList nodes = root.SelectNodes("/r");
                    string edition = null;
                    string versionraw = null;
                    string ComputerName = null;
                    string serverName = null;
                    foreach (XmlNode node in nodes)
                    {
                        textBox2.Clear();
                        serverName = node["serverName"].InnerText;
                        versionraw = node["version"].InnerText;
                        string serverState = node["serverState"].InnerText;
                        edition = node["edition"].InnerText;
                        ComputerName = node["computerName"].InnerText;
                        textBox2.Visible = true;
                        textBox2.AppendText("Имя сервера: " + serverName + Environment.NewLine);
                        textBox2.AppendText("Версия iiko: " + versionraw + Environment.NewLine);
                        textBox2.AppendText("Статус сервера: " + serverState + Environment.NewLine);
                        textBox2.AppendText("Тип: " + edition + Environment.NewLine);
                        button17.Visible = true;
                    }
                    string rms = null;
                    string folder = null;
                    if (edition.Contains("default"))
                    {
                        rms = "RMS";
                        folder = "Office";
                    }
                    if (edition.Contains("chain"))
                    {
                        rms = "Chain";
                        folder = "Coffice";
                    }
                    string versiondot = versionraw.Replace(".", string.Empty);
                    string version = versiondot.Substring(0, 3);
                    string dir = path + @"/" + rms + @"/" + folder + version + @"/" + "BackOffice.exe";
                    Utilities.CofnigBackOffice(url, port, http, dir, edition, versionraw, ComputerName, serverName);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError("Getiikoversionxml.RunBatchFile", ex.Message, ex.StackTrace);
                }
            }
            //Конец открывашки бека
        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            try
            {
                button24.Visible = true;
                string path = File.ReadAllText(Helper.ProgramData + @"\CLEAR_bat\bin\BackPath.txt");
                button23.Visible = true;
                label3.Text = "Укажи путь до папок с беками" + Environment.NewLine + "Выбрать папку которая содержит Chain и RMS" + Environment.NewLine + "Названия: officeXXX -RMS; CofficeXXX -Chain" + Environment.NewLine + "Папка сейчас: " + path;
                label3.Visible = true;
                var filesWithoutExtension = Directory.GetDirectories(@"C:/Program Files/iiko/iikoRMS/Front.Net/Plugins/");
                string backfolder = null;
                foreach (string folder in filesWithoutExtension)
                {
                    backfolder = (Path.GetFileName(folder));
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Getiikoversionxml.RunBatchFile", ex.Message, ex.StackTrace);
            }
        }

        private void button24_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog Dir = new FolderBrowserDialog();
            if (Dir.ShowDialog() == DialogResult.OK)
            {
                System.IO.Directory.GetFiles(Dir.SelectedPath);
                File.WriteAllText(Helper.ProgramData + @"\CLEAR_bat\bin\BackPath.txt", Dir.SelectedPath);

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            button24.Visible = false;
            button23.Visible = false;
            label3.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            button25Clicked = true;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            button26Clicked = true;
    }
    }
}