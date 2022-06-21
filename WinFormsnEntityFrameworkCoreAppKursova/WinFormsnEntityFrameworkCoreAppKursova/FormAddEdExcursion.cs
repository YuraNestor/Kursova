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
    
    public partial class FormAddEdExcursion : Form
    {
        
        public FormAddEdExcursion(int selected)
        {
            InitializeComponent();
            //label1.Text =Selectedinformation;
            label1.Text = selected.ToString();
            DownInitialize();
        }
        public int selected;
        public Excursion excursion1;
        private bool checkFreeDate(DateTime dateTime, int dr, DateTime dateTime2, int dr2)
        {
            DateTime ans = dateTime.AddMinutes(dr);
            DateTime ans2 = dateTime2.AddMinutes(dr2);
            if (ans < dateTime2 || dateTime > ans2)
            {
                return true;
            }
            return false;
        }
        private Bus checkBus(Bus bus, DateTime dateTime, int dr)
        {
            foreach (Excursion BusExcursion in bus.Excursions)
            {
                if(bus.Excursions.Count == 0)
                {
                    return bus;
                }
                if (!checkFreeDate(BusExcursion.DateOfExcursions, BusExcursion.Duration, dateTime, dr))
                {
                    return null;
                }
            }
            return bus;
        }

        private void DownInitialize()
        {
            using(ExcursionContext context = new ExcursionContext())
            {
                comboBox1.Items.Clear();

                foreach (Customer customer in context.Customers.ToList())
                {
                    if (customer.Excursions.Count != 0)
                    {
                        if (customer.Excursions.Last().DateOfExcursions.AddDays(30) < DateTime.Now)
                        {
                            if (customer.Deposit < 0)
                            {
                                continue;
                            }
                        }
                    }
                    
                    comboBox1.Items.Add(customer);
                }
                comboBox2.Items.Clear();

                foreach (ExcursionType exctype in context.ExcursionTypes)
                {
                    comboBox2.Items.Add(exctype);
                }
                checkedListBox1.Items.Clear();
                if (this.Text == "Add Excursion")
                {
                    foreach (Bus bus in context.Buses)
                    {

                        checkedListBox1.Items.Add(bus);
                    }
                }

                
            }
            

        }
        private bool AdequacyBusesForTurists(List<Bus> buses, int amount)
        {
            int busAmount = 0;
            int i = 1;
            foreach (Bus bus in buses)
            {
                busAmount += bus.Capacity;
                if (busAmount >= amount)
                {
                    if (i == buses.Count)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show((buses.Count - i).ToString() + "Extra buses ");
                        return false;
                    }
                }
                i++;
            }
            MessageBox.Show("Not enough " + (amount-busAmount).ToString() + " seats");
            return false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
            //foreach(TextBox textBox in this.Controls.OfType<TextBox>())
            //{
            //    if (string.IsNullOrEmpty(textBox.Text))
            //    {
            //        return;
            //    }
            //}
            foreach (Control x in this.Controls)
            {
                if (x is TextBox)
                {
                    if (string.IsNullOrEmpty(((TextBox)x).Text))
                    {
                        return;
                    }
                    
                }
                if(x is NumericUpDown)
                {
                   if(((NumericUpDown)x).Value == 0)
                    {
                        return ;
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
                    List<Bus> buses = new List<Bus>();
                    
                    foreach (Bus bus in ((CheckedListBox)x).CheckedItems)
                    {
                        buses.Add(bus);
                    }
                    if (!AdequacyBusesForTurists(buses, Convert.ToInt32(numericUpDown3.Value)))
                    {
                        return;
                    }
                }

            }
            
            this.DialogResult = DialogResult.Yes;
            



        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            using (ExcursionContext context = new ExcursionContext())
            {
                int Id;
                if(comboBox2.SelectedItem != null)
                {
                     Id= (comboBox2.SelectedItem as ExcursionType).Id;
                }
                else
                {
                    return;
                }
                List<Bus> ThisTypeBuses = context.ExcursionTypes.Find(Id).Buses;
                foreach (Bus bus in ThisTypeBuses)
                {

                    Bus bus1 = checkBus(bus, dateTimePicker1.Value, Convert.ToInt32(numericUpDown1.Value));
                    if(bus1 != null)
                    {
                        checkedListBox1.Items.Add(bus1);
                    }
                    
                    

                    
                }
                if (excursion1 != null)
                {
                    foreach(Bus bus in excursion1.Buses)
                    {
                        //Id = (comboBox2.SelectedItem as ExcursionType).Id;
                        bool flag=false;
                        if(!checkFreeDate(excursion1.DateOfExcursions, excursion1.Duration, dateTimePicker1.Value, Convert.ToInt32(numericUpDown1.Value))){
                            foreach (ExcursionType excursionType in bus.ExcursionTypes)
                            {
                                if (excursionType.Id == (comboBox2.SelectedItem as ExcursionType).Id)
                                {
                                    flag = true; break;
                                }
                            }
                            if (flag)
                            {
                                checkedListBox1.Items.Add(bus, true);
                            }
                        }
                        
                        
                    }
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox2.Refresh();
            checkedListBox1.Items.Clear();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
        }
    }
}
