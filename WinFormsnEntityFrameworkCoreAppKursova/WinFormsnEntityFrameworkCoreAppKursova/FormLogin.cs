using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsnEntityFrameworkCoreAppKursova
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            Adminslogins.Add("Petro");
            UsualLogins.Add("User");
            AdminsPussWord.Add("123");
            UsualPassWord.Add("123");
        }

        private List<string> Adminslogins=new List<string>();
        private List<string> UsualLogins = new List<string>();
        private List<string> AdminsPussWord = new List<string>();
        private List<string> UsualPassWord = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach(string s in Adminslogins)
            {
                if (s == textBox1.Text && AdminsPussWord[i]==textBox2.Text)
                {
                    Form1 form1=new Form1("Admin");
                    this.Visible = false;
                    form1.ShowDialog();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    this.Visible = true;
                    return;
                   

                }
                i++;
            }
            i = 0;
            foreach (string s in UsualLogins)
            {
                if (s == textBox1.Text && UsualPassWord[i] == textBox2.Text)
                {
                    Form1 form1 = new Form1("Usual");
                    this.Visible = false;
                    form1.ShowDialog();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    this.Visible=true;
                    return;
                }
                i++;
            }
            MessageBox.Show("Incorrect data");
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
