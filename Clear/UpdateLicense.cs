using RestSharp;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Clear
{
    public partial class UpdateLicense : Form
    {
        public UpdateLicense()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            porttxt.Text = "443";
            label6.Text = "https://";
        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string login = logintxt.Text;
                string password = pswtext.Text;
                string port = porttxt.Text;
                string adress = adrtext.Text;
                string http = label6.Text;
                string type = "BACK";
                string method = "/resto/services/authorization?methodName=authorize&";
                //SHA1
                string passwordsha1 = null;
                using (SHA1 sha1Hash = SHA1.Create())
                {
                    //From String to byte array
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                    byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                    string sha1 = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                    // exp
                    textBox1.AppendText("SHA1 Password: " + sha1 + Environment.NewLine);
                    passwordsha1 = sha1.ToLower();
                    textBox1.AppendText("SHA1 Lower: " + passwordsha1 + Environment.NewLine);
                }

                //Получаем инфу с сервера
                XmlDocument doc1 = new XmlDocument();
                doc1.Load(http + adress + ":" + port + "/resto/get_server_info.jsp?encoding=UTF-8");
                XmlElement root = doc1.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/r");

                string backversion = null;
                string editioniiko = null;
                foreach (XmlNode node in nodes)
                {
                    string serverName = node["serverName"].InnerText;
                    backversion = node["version"].InnerText;
                    string serverState = node["serverState"].InnerText;
                    string edition = node["edition"].InnerText;
                    textBox1.AppendText("Имя сервера: " + serverName + Environment.NewLine);
                    textBox1.AppendText("Версия iiko: " + backversion + Environment.NewLine);
                    textBox1.AppendText("Статус сервера: " + serverState + Environment.NewLine);
                    editioniiko = edition.Replace("default", "IIKO_RMS").Replace("chain", "IIKO_CHAIN");
                    textBox1.AppendText("Версия iiko: " + editioniiko + Environment.NewLine);

                }
                // Отправка POST
                string rawlic = null;
                {
                    var client = new RestClient(http + adress + ":" + port + method);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "text/xml");
                    request.AddHeader("X-Resto-LoginName", login);
                    request.AddHeader("X-Resto-PasswordHash", passwordsha1);
                    request.AddHeader("X-Resto-BackVersion", backversion);
                    request.AddHeader("X-Resto-AuthType", type);
                    request.AddHeader("X-Resto-ServerEdition", editioniiko);
                    request.AddHeader("Connection", "close");
                    var body = @"<?xml version=""1.0"" encoding=""utf-8""?>
" + "\n" +
                    @"<args>
" + "\n" +
                    @"    <entities-version>1806899</entities-version>
" + "\n" +
                    @"    <client-type>BACK</client-type>
" + "\n" +
                    @"    <enable-warnings>false</enable-warnings>
" + "\n" +
                    @"    <client-call-id>30264dfd-570d-46c0-81b8-6bef9da5a2c9</client-call-id>
" + "\n" +
                    @"    <license-hash>-1938788177</license-hash>
" + "\n" +
                    @"    <restrictions-state-hash>5761</restrictions-state-hash>
" + "\n" +
                    @"    <obtained-license-connections-ids />
" + "\n" +
                    @"    <request-watchdog-check-results>true</request-watchdog-check-results>
" + "\n" +
                    @"    <use-raw-entities>true</use-raw-entities>
" + "\n" +
                    @"</args>";
                    request.AddParameter("text/xml", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    //exp
                    string responseraw = response.Content.Replace("&lt;", "<").Replace("&gt;", ">");

                    rawlic = responseraw;
                }
                //Перевожу всю херню в порядок
                string path = Helper.ProgramData + @"\CLEAR_bat\bat\iiko_license.txt";
                File.WriteAllText(path, rawlic);
                // получение списка названий
                //..........................................................................................................


                XmlDocument error_doc1 = new XmlDocument();
                error_doc1.Load(http + adress + ":" + port + "/resto/get_server_info.jsp?encoding=UTF-8");
                XmlElement error_root = error_doc1.DocumentElement;
                XmlNodeList error_nodes = error_root.SelectNodes("/r");
                string resultStatus = null;
                foreach (XmlNode error_node in error_nodes)
                {
                    resultStatus = error_node["resultStatus"].InnerText;
                    textBox1.AppendText("Ответ от сервера: " + resultStatus + Environment.NewLine);
                }
                if (resultStatus.Contains("SUCCES"))
                {
                    textBox1.AppendText("Лицензии получены");
                }
                else
                {
                    textBox1.AppendText("Ошибка: " + resultStatus);
                }
                //..........................................................................................................
                //Конец
            }
            catch (Exception ex)
            {
                textBox1.AppendText("Ошибка");
                ErrorLogger.LogError("UPDLIC.RunBatchFile", ex.Message, ex.StackTrace);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string login = logintxt.Text;
                string password = pswtext.Text;
                string port = porttxt.Text;
                string adress = adrtext.Text;
                string http = label6.Text;
                string type = "BACK";
                string method = "/resto/services/licensing?methodName=getForceDeveloperSandboxModeInfo&";
                //SHA1
                string passwordsha1 = null;
                using (SHA1 sha1Hash = SHA1.Create())
                {
                    //From String to byte array
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                    byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                    string sha1 = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                    // exp
                    textBox1.AppendText("SHA1 Password: " + sha1 + Environment.NewLine);
                    passwordsha1 = sha1.ToLower();
                    textBox1.AppendText("SHA1 Lower: " + passwordsha1 + Environment.NewLine);
                }

                //Получаем инфу с сервера
                XmlDocument doc1 = new XmlDocument();
                doc1.Load(http + adress + ":" + port + "/resto/get_server_info.jsp?encoding=UTF-8");
                XmlElement root = doc1.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/r");

                string backversion = null;
                string editioniiko = null;
                foreach (XmlNode node in nodes)
                {
                    string serverName = node["serverName"].InnerText;
                    backversion = node["version"].InnerText;
                    string serverState = node["serverState"].InnerText;
                    string edition = node["edition"].InnerText;
                    textBox1.AppendText("Имя сервера: " + serverName + Environment.NewLine);
                    textBox1.AppendText("Версия iiko: " + backversion + Environment.NewLine);
                    textBox1.AppendText("Статус сервера: " + serverState + Environment.NewLine);
                    editioniiko = edition.Replace("default", "IIKO_RMS").Replace("chain", "IIKO_CHAIN");
                    textBox1.AppendText("Версия iiko: " + editioniiko + Environment.NewLine);

                }
                // Отправка POST
                string rawlic = null;
                {
                    var client = new RestClient(http + adress + ":" + port + method);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "text/xml");
                    request.AddHeader("X-Resto-LoginName", login);
                    request.AddHeader("X-Resto-PasswordHash", passwordsha1);
                    request.AddHeader("X-Resto-BackVersion", backversion);
                    request.AddHeader("X-Resto-AuthType", type);
                    request.AddHeader("X-Resto-ServerEdition", editioniiko);
                    request.AddHeader("Connection", "close");
                    var body = @"<?xml version=""1.0"" encoding=""utf-8""?>
" + "\n" +
                    @"<args>
" + "\n" +
                    @"    <entities-version>1806899</entities-version>
" + "\n" +
                    @"    <client-type>BACK</client-type>
" + "\n" +
                    @"    <enable-warnings>false</enable-warnings>
" + "\n" +
                    @"    <client-call-id>30264dfd-570d-46c0-81b8-6bef9da5a2c9</client-call-id>
" + "\n" +
                    @"    <license-hash>-1938788177</license-hash>
" + "\n" +
                    @"    <restrictions-state-hash>5761</restrictions-state-hash>
" + "\n" +
                    @"    <obtained-license-connections-ids />
" + "\n" +
                    @"    <request-watchdog-check-results>true</request-watchdog-check-results>
" + "\n" +
                    @"    <use-raw-entities>true</use-raw-entities>
" + "\n" +
                    @"</args>";
                    request.AddParameter("text/xml", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    //exp
                    string responseraw = response.Content.Replace("&lt;", "<").Replace("&gt;", ">");

                    rawlic = responseraw;
                }
                //Перевожу всю херню в порядок
                string path = Helper.ProgramData + @"\CLEAR_bat\bat\iiko_license.txt";
                File.WriteAllText(path, rawlic);
                // получение списка названий
                //..........................................................................................................
                textBox1.AppendText("Лицензии обновлены");
                //..........................................................................................................
                //Конец
            }
            catch (Exception ex)
            {
                textBox1.AppendText("Ошибка: " + Environment.NewLine + ex.Message+ Environment.NewLine + ex.StackTrace);
                ErrorLogger.LogError("UPDLIC.RunBatchFile", ex.Message, ex.StackTrace);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (httpbox.Checked == true)
            {
                label6.Text = "http://";
                porttxt.Clear();
                label3.Visible = true;
                porttxt.Visible = true;
            }
            else
            {
                label6.Text = "https://";
                label3.Visible = false;
                porttxt.Visible = false;
                porttxt.Text = "443";
            }
        }

        private void logintxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void pswtext_TextChanged(object sender, EventArgs e)
        {

        }

        private void porttxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void adrtext_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
