using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsnEntityFrameworkCoreAppKursova.Models;

namespace WinFormsnEntityFrameworkCoreAppKursova
{
    public partial class FormZvit : Form
    {
        public FormZvit()
        {
            InitializeComponent();

            View2Refresh();
        }
        private void View2Refresh()
        {
            listView2.Items.Clear();
            using (ExcursionContext context = new ExcursionContext())
            {

                var excursions = context.Excursions.ToList();
                DateTime datePicker = dateTimePicker1.Value;
               
                DateTime lastDate = datePicker.AddDays(30);
                decimal monthProfit = 0;
                for(DateTime i= datePicker; i<lastDate; i=i.AddDays(1))
                {
                    ListViewItem item = new ListViewItem(i.ToString());
                    int cout=0;
                    decimal totalPrice = 0;
                    foreach(Excursion excursion in excursions)
                    {

                        
                        
                        if (excursion.DateOfExcursions.ToString("MM/dd/yyyy")==i.ToString("MM/dd/yyyy"))
                        {
                            cout++;
                            totalPrice+=excursion.Price;
                            
                        }
                    }
                    monthProfit += totalPrice;
                    item.SubItems.Add(cout.ToString());
                    item.SubItems.Add(totalPrice.ToString());
                    listView2.Items.Add(item);
                }
                label1.Text = "Month profit " + monthProfit.ToString();
                //foreach (var excursion in excursions)
                //{

                //    ListViewItem item = new ListViewItem(customer.Id.ToString());

                //    item.SubItems.Add(customer.Name);
                //    item.SubItems.Add(customer.Deposit.ToString());



                //    listView2.Items.Add(item);



                //}
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            View2Refresh();
        }
    }
}
