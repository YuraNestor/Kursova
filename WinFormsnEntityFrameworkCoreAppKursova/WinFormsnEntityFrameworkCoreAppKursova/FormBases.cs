using Microsoft.EntityFrameworkCore;
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
    public partial class FormBases : Form
    {
        public FormBases()
        {
            InitializeComponent();
            View2Refresh();
        }
        public FormBases(string userRole)
        {
            UserRole = userRole;
            InitializeComponent();
            View2Refresh();
            if (UserRole != "Admin")
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }
        private string UserRole;
        private void View2Refresh()
        {
            listView2.Items.Clear();
            using (ExcursionContext context=new ExcursionContext())
            {
                var buses=context.Buses.ToList();
                foreach (var bus in buses)
                {

                    ListViewItem item = new ListViewItem(bus.Id.ToString());

                    item.SubItems.Add(bus.Brand + " " + bus.Model);
                    item.SubItems.Add(bus.BDriver.Name);
                    item.SubItems.Add(bus.Capacity.ToString());
                    item.SubItems.Add(bus.FuelConsumption.ToString());
                    var et = bus.ExcursionTypes;
                    string types = string.Empty;
                    foreach (var type in et)
                    {
                        types += type.ToString() + ", ";
                    }
                    item.SubItems.Add(types);

                    listView2.Items.Add(item);



                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
                //button2.Visible = false;
                listView1.Items.Clear();
                return;
            }
            using (ExcursionContext excursionContext = new ExcursionContext())
            {

                listView1.Items.Clear();
                int Id = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                Bus bus = excursionContext.Buses.Find(Id);
                if(bus == null)
                {
                    return;
                    listView1.Items.Clear();
                }
                var exc =bus.Excursions.ToList();
                foreach (var excursion in exc)
                {

                    ListViewItem item = new ListViewItem(excursion.Id.ToString());

                    item.SubItems.Add(excursion.Name);
                    item.SubItems.Add(excursion.ExcCustomer.Name);
                    item.SubItems.Add(excursion.DateOfExcursions.ToString());
                    item.SubItems.Add(excursion.Duration.ToString());
                    item.SubItems.Add(excursion.Destination);
                    item.SubItems.Add(excursion.Distance.ToString());
                    item.SubItems.Add(excursion.NumberOfTourists.ToString());
                    item.SubItems.Add(excursion.Price.ToString());
                    item.SubItems.Add(excursion.ExcType.Name);
                    listView1.Items.Add(item);



                }
            }
        }
        
        private void button2_Click(object sender, EventArgs e) //add bus
        {
            using (ExcursionContext context = new ExcursionContext())
            {
                FormAddOrEditOther formAddBus = new FormAddOrEditOther();
                formAddBus.Text = (sender as Button).Text;
                int intervalY = 41;
                
                
                TextBox textBoxModel= new TextBox();
                
                textBoxModel.Location = new System.Drawing.Point(24, intervalY+=41);
                Label label2=new Label();
                label2.Location = new System.Drawing.Point(155, intervalY);
                label2.Text = "Model";
                ComboBox comboBoxDrivers = new ComboBox();
                comboBoxDrivers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                comboBoxDrivers.Location = new System.Drawing.Point(24, intervalY += 41);
                Label label3 = new Label();
                label3.Location = new System.Drawing.Point(155, intervalY);
                label3.Text = "Driver";
                List<Driver> freeDrivers = new List<Driver>();
                foreach(Driver driver in context.Drivers.ToList())
                {
                    if (driver.DBus == null)
                    {
                        freeDrivers.Add(driver);
                    }
                }
                if(freeDrivers.Count == 0)
                {
                    MessageBox.Show("No free drivers \nAdd drivers from the beginning");
                    return;
                }
                //comboBoxDrivers.Items.AddRange(freeDrivers.ToArray());
                foreach(Driver driver in freeDrivers)
                {
                    comboBoxDrivers.Items.Add(driver);
                }
                NumericUpDown numericUpDownCap= new NumericUpDown();
                numericUpDownCap.Location = new System.Drawing.Point(24, intervalY += 41);
                Label label4 = new Label();
                label4.Location = new System.Drawing.Point(155, intervalY);
                label4.Text = "Capacity";
                NumericUpDown numericUpDownFuel= new NumericUpDown();
                numericUpDownFuel.Location = new System.Drawing.Point(24, intervalY += 41);
                Label label5 = new Label();
                label5.Location = new System.Drawing.Point(155, intervalY);
                label5.Text = "Fuel Consumption";
                CheckedListBox checkedListBoxTypes = new CheckedListBox();
                checkedListBoxTypes.Location = new System.Drawing.Point(24, intervalY += 41);
                Label label6 = new Label();
                label6.Location = new System.Drawing.Point(155, intervalY);
                label6.Text = "Excursion Types";
                if (context.ExcursionTypes.ToList().Count == 0)
                {
                    MessageBox.Show("No excursion tipes \nAdd excursion tipes from the beginning");
                    return;
                }
                foreach (ExcursionType excursionType in context.ExcursionTypes.ToList())
                    checkedListBoxTypes.Items.Add(excursionType);
                formAddBus.Controls.Add(textBoxModel);
                formAddBus.Controls.Add(comboBoxDrivers);
                formAddBus.Controls.Add(numericUpDownCap);
                formAddBus.Controls.Add(numericUpDownFuel);
                formAddBus.Controls.Add(checkedListBoxTypes);
                formAddBus.Controls.Add(label2);
                formAddBus.Controls.Add(label3);
                formAddBus.Controls.Add(label4);
                formAddBus.Controls.Add(label5);
                formAddBus.Controls.Add(label6);

                if (formAddBus.ShowDialog(this) != DialogResult.Yes)
                    return;
                Bus bus = new Bus();
                bus.Brand = formAddBus.textBoxName.Text;
                bus.Model = textBoxModel.Text;
                bus.Capacity = Convert.ToInt32(numericUpDownCap.Value);
                bus.FuelConsumption= Convert.ToInt32(numericUpDownFuel.Value);
                int Id = (comboBoxDrivers.SelectedItem as Driver).Id;
                bus.BDriver = context.Drivers.Find(Id);
                foreach(ExcursionType excursionType1 in checkedListBoxTypes.CheckedItems)
                {
                    Id = excursionType1.Id;
                    bus.ExcursionTypes.Add(context.ExcursionTypes.Find(Id));
                }


                context.Buses.Add(bus);
                context.SaveChanges();
                View2Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e) //edit bus
        {
            using (ExcursionContext context = new ExcursionContext())
            {
                FormAddOrEditOther formAddBus = new FormAddOrEditOther();
                formAddBus.Text = (sender as Button).Text;
                if (listView2.SelectedItems.Count == 0)
                {
                    MessageBox.Show("choose bus");
                    return;
                }
                int Id = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                Bus bus = context.Buses.Find(Id);
                int intervalY = 41;
                if (bus.Excursions.Count != 0/*|| bus.BDriver!=null*/)
                {
                    var dr = MessageBox.Show("If you change this bus you will make changes to the existing data", "Edit", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No || dr == DialogResult.OK)
                        return;
                }
                formAddBus.textBoxName.Text = bus.Brand;
                TextBox textBoxModel = new TextBox();

                textBoxModel.Location = new System.Drawing.Point(24, intervalY += 41);
                textBoxModel.Text = bus.Model;
                Label label2 = new Label();
                label2.Location = new System.Drawing.Point(155, intervalY);
                label2.Text = "Model";
                ComboBox comboBoxDrivers = new ComboBox();
                comboBoxDrivers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                comboBoxDrivers.Location = new System.Drawing.Point(24, intervalY += 41);

                Label label3 = new Label();
                label3.Location = new System.Drawing.Point(155, intervalY);
                label3.Text = "Driver";
                List<Driver> freeDrivers = new List<Driver>();
                foreach (Driver driver in context.Drivers.ToList())
                {
                    if (driver.DBus == null)
                    {
                        freeDrivers.Add(driver);
                    }
                }
                //if (freeDrivers.Count == 0)
                //{
                //    MessageBox.Show("No free drivers \nAdd drivers from the beginning");
                //    return;
                //}
                //comboBoxDrivers.Items.AddRange(freeDrivers.ToArray());
                comboBoxDrivers.Items.Add(bus.BDriver);
                foreach (Driver driver in freeDrivers)
                {
                    comboBoxDrivers.Items.Add(driver);
                }
                comboBoxDrivers.SelectedIndex = 0;
                NumericUpDown numericUpDownCap = new NumericUpDown();
                numericUpDownCap.Value = bus.Capacity;
                numericUpDownCap.Location = new System.Drawing.Point(24, intervalY += 41);
                Label label4 = new Label();
                label4.Location = new System.Drawing.Point(155, intervalY);
                label4.Text = "Capacity";
                NumericUpDown numericUpDownFuel = new NumericUpDown();
                numericUpDownFuel.Value = bus.FuelConsumption;
                numericUpDownFuel.Location = new System.Drawing.Point(24, intervalY += 41);
                Label label5 = new Label();
                label5.Location = new System.Drawing.Point(155, intervalY);
                label5.Text = "Fuel Consumption";
                CheckedListBox checkedListBoxTypes = new CheckedListBox();
                checkedListBoxTypes.Location = new System.Drawing.Point(24, intervalY += 41);
                Label label6 = new Label();
                label6.Location = new System.Drawing.Point(155, intervalY);
                label6.Text = "Excursion Types";
                if (context.ExcursionTypes.ToList().Count == 0)
                {
                    MessageBox.Show("No excursion tipes \nAdd excursion tipes from the beginning");
                    return;
                }
                
                List<ExcursionType> BusExcursionTypes = bus.ExcursionTypes;
                foreach (ExcursionType excursionType in context.ExcursionTypes.ToList())
                {
                    bool flag = false;
                    foreach (ExcursionType BusExcursionType in BusExcursionTypes)
                    {

                        if (excursionType.Id == BusExcursionType.Id)
                        {
                            flag = true;
                            break;
                        }


                    }

                    checkedListBoxTypes.Items.Add(excursionType, flag);




                }
                formAddBus.Controls.Add(textBoxModel);
                formAddBus.Controls.Add(comboBoxDrivers);
                formAddBus.Controls.Add(numericUpDownCap);
                formAddBus.Controls.Add(numericUpDownFuel);
                formAddBus.Controls.Add(checkedListBoxTypes);
                formAddBus.Controls.Add(label2);
                formAddBus.Controls.Add(label3);
                formAddBus.Controls.Add(label4);
                formAddBus.Controls.Add(label5);
                formAddBus.Controls.Add(label6);

                if (formAddBus.ShowDialog(this) != DialogResult.Yes)
                    return;
                //Bus bus = new Bus();
                bus.Brand = formAddBus.textBoxName.Text;
                bus.Model = textBoxModel.Text;
                bus.Capacity = Convert.ToInt32(numericUpDownCap.Value);
                bus.FuelConsumption = Convert.ToInt32(numericUpDownFuel.Value);
                Id = (comboBoxDrivers.SelectedItem as Driver).Id;
                bus.BDriver = context.Drivers.Find(Id);
                bus.ExcursionTypes.Clear();
                var cr = checkedListBoxTypes.CheckedItems;
                foreach (var excursionType1 in cr)
                {
                    Id = (excursionType1 as ExcursionType).Id;
                    bus.ExcursionTypes.Add(context.ExcursionTypes.Find(Id));
                }


                context.Entry(bus).State = EntityState.Modified;
                context.SaveChanges();
                View2Refresh();
            }
        }
        protected internal bool DeleteBus(Bus bus , ExcursionContext context)
        {
            
                
                List<Excursion> excursionsToRemove = new List<Excursion>();
                if (bus.Excursions.Count != 0)
                {
                    var dr = MessageBox.Show("If you change this bus you will make changes to the existing data", "changes data", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No || dr == DialogResult.OK)
                        return false;
                    string strExcursions = "\n";

                    foreach (Excursion excursion in bus.Excursions)
                    {
                        if (excursion.Buses.Count == 1)
                        {
                            strExcursions += excursion.ToString() + " ";
                            excursionsToRemove.Add(excursion);
                        }

                    }
                    if (excursionsToRemove.Count != 0)
                    {
                        //strExcursions += ;
                        var dr2 = MessageBox.Show("If delete this bus to be removed" + excursionsToRemove.Count.ToString() + "Excursions: " + strExcursions, "Confirmation", MessageBoxButtons.YesNo);
                        if (dr2 == DialogResult.No || dr2 == DialogResult.OK)
                            return false;
                        dr = MessageBox.Show("Return Customer's money ", "Confirmation", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No || dr == DialogResult.OK)
                        {

                        }
                        else
                        {
                            foreach(Excursion excursion in excursionsToRemove)
                            {
                                excursion.ExcCustomer.Deposit += excursion.Price;
                            }
                                
                        }
                    context.Excursions.RemoveRange(excursionsToRemove);
                        context.Buses.Remove(bus);
                        
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        
                        context.Buses.Remove(bus);

                        context.SaveChanges();
                        return true;
                    }



                    //MessageBox.Show("Realy want"+dr.ToString());
                }
                else
                {
                    context.Buses.Remove(bus);

                    context.SaveChanges();
                    return true;
                }
                //context.Entry(excursionType).State = EntityState.Modified;
                //context.SaveChanges();
                
                //listView3.Items.Clear();
            

        }
        private void button4_Click(object sender, EventArgs e) //delete bus
        {
            try
            {
                using (ExcursionContext context = new ExcursionContext())
                {
                    if (listView2.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("choose bus");
                        return;
                    }
                    int Id = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                    DeleteBus(context.Buses.Find(Id), context);
                    View2Refresh();
                    listView1.Items.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Fail delete");
            }
        }
    }
    
}
