using System;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace Clear
{

    public partial class Info : Form
    {

        public Info()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

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
            Info win = new Info();
            win.Show();
            Hide();
            Utilities.RunBatchFile(Required.ScriptsFolder + "clean_all.bat");
            win.Close();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void front_button_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/rlato");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Info win = new Info();
            win.Show();
            Hide();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Info win = new Info();
            win.Show();
            Hide();
            Utilities.RunBatchFile(Required.ScriptsFolder + "clean_wo_pl.bat");
            win.Close();
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }
    }
}