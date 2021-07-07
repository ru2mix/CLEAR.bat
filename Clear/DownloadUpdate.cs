using System;
using System.Windows.Forms;


namespace Clear
{
    public partial class DownloadUpdate : Form
    {
        public DownloadUpdate()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void DownloadUpdate_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Перехожу в главное меню
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Скачиваю обновление
            Utilities.RunBatchFile(Required.ScriptsFolder + "rmdir.bat");
            System.Diagnostics.Process.Start("https://github.com/ru2mix/CLEAR.bat/raw/main/CLEAR.bat.exe");
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
