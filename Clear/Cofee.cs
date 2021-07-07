using System;
using System.Windows.Forms;
using System.Reflection;
using System.Net;

namespace Clear
{
    public partial class Cofee : Form
    {
        public Cofee()
        {
            //Загрузка формы
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.SelectionLength = 0;
            //проверка на наличие новой версии


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateLicense updlic = new UpdateLicense();
            this.Hide();
            updlic.ShowDialog();
            this.Show();
        }
    }
}
