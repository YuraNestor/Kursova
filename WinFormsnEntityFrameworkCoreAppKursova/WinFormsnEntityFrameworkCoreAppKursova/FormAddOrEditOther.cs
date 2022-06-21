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
    public partial class FormAddOrEditOther : Form
    {
        public FormAddOrEditOther()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control x in this.Controls)
            {
                if (x is TextBox)
                {
                    if (string.IsNullOrEmpty(((TextBox)x).Text))
                    {
                        return;
                    }

                }
                if (x is NumericUpDown)
                {
                    if (((NumericUpDown)x).Value == 0)
                    {
                        return;
                    }
                }
                if (x is ComboBox)
                {
                    if (((ComboBox)x).SelectedItem == null)
                    {
                        return;

                    }
                }
                if (x is CheckedListBox)
                {
                    if (((CheckedListBox)x).CheckedItems.Count == 0)
                    {
                        return;

                    }
                }

            }
            this.DialogResult = DialogResult.Yes;
        }
    }
}
