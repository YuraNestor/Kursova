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
    public partial class FormDrivers : Form
    {
        public FormDrivers()
        {
            InitializeComponent();
            View2Refresh();
        }
        public FormDrivers(string userRole)
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
            using (ExcursionContext context = new ExcursionContext())
            {
                var drivers = context.Drivers.ToList();
                foreach (var driver in drivers)
                {

                    ListViewItem item = new ListViewItem(driver.Id.ToString());

                    item.SubItems.Add(driver.Name);




                    listView2.Items.Add(item);



                }
            }
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedIndices.Count == 0)
            {
                button3.Visible = button4.Visible = false;
                //listView1.Items.Clear();
                return;
            }
            using (ExcursionContext excursionContext = new ExcursionContext())
            {
                button3.Visible = button4.Visible = true;
                //try
                //{

                //    //listView1.Items.Clear();
                //    var exc = excursionContext.ExcursionTypes.ToList()[listView2.SelectedIndices[0]].Excursions.ToList();
                //    foreach (var excursion in exc)
                //    {

                //        ListViewItem item = new ListViewItem(excursion.Id.ToString());

                //        item.SubItems.Add(excursion.Name);
                //        item.SubItems.Add(excursion.ExcCustomer.Name);
                //        item.SubItems.Add(excursion.DateOfExcursions.ToString());
                //        item.SubItems.Add(excursion.Duration.ToString());
                //        item.SubItems.Add(excursion.Destination);
                //        item.SubItems.Add(excursion.Distance.ToString());
                //        item.SubItems.Add(excursion.NumberOfTourists.ToString());
                //        item.SubItems.Add(excursion.Price.ToString());
                //        item.SubItems.Add(excursion.ExcType.Name);
                //        //listView1.Items.Add(item);



                //    }



                //}
                //catch
                //{
                //    //listView1.Items.Clear();
                //}
                listView3.Items.Clear();
                var bus = excursionContext.Drivers.ToList()[listView2.SelectedIndices[0]].DBus;
                try
                {

                    if(bus == null)
                    {
                        listView3.Items.Clear();
                        return;
                    }
                    ListViewItem item = new ListViewItem(bus.Id.ToString());

                    item.SubItems.Add(bus.Brand + " " + bus.Model);
                    item.SubItems.Add(bus.BDriver.Name);
                    item.SubItems.Add(bus.Capacity.ToString());
                    item.SubItems.Add(bus.FuelConsumption.ToString());
                    var et = bus.ExcursionTypes;
                    string types = string.Empty;
                    foreach (var type in et)
                    {
                        types += type.ToString();
                    }
                    item.SubItems.Add(types);

                    listView3.Items.Add(item);

                }
                catch
                {
                    listView3.Items.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//add driver
        {
            using (ExcursionContext context = new ExcursionContext())
            {
                FormAddOrEditOther formAddDriver = new FormAddOrEditOther();
                formAddDriver.Text = (sender as Button).Text;
                if (formAddDriver.ShowDialog(this) != DialogResult.Yes)
                    return;
                Driver driver = new Driver();
                driver.Name = formAddDriver.textBoxName.Text;
                context.Drivers.Add(driver);
                context.SaveChanges();
                View2Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e) //edit driver
        {
            try
            {
                using (ExcursionContext context = new ExcursionContext())
                {
                    FormAddOrEditOther formEditDriver = new FormAddOrEditOther();
                    formEditDriver.Text = (sender as Button).Text;
                    int Id = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                    Driver driver = context.Drivers.Find(Id);
                    if (driver.DBus != null)
                    {
                        var dr = MessageBox.Show("If you change this type you will make changes to the existing data", "Edit", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No || dr == DialogResult.OK)
                            return;
                    }
                    formEditDriver.textBoxName.Text = driver.Name;
                    if (formEditDriver.ShowDialog(this) != DialogResult.Yes)
                        return;

                    driver.Name = formEditDriver.textBoxName.Text;
                    context.Entry(driver).State = EntityState.Modified;
                    context.SaveChanges();
                    View2Refresh();
                }
            }
            catch
            {
                MessageBox.Show("Fail edit choose element!");
            }
        }

        private void button4_Click(object sender, EventArgs e)//delete driver
        {
            try
            {
                using (ExcursionContext context = new ExcursionContext())
                {

                    int Id = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                    Driver driver = context.Drivers.Find(Id);
                    if (driver.DBus != null)
                    {
                        string strBuses = "\nBus: "+driver.DBus.ToString();
                        
                        
                        var dr = MessageBox.Show("If delete this type to be removed" + strBuses , "Confirmation", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No || dr == DialogResult.OK)
                            return;

                        //context.Buses.Remove(driver.DBus);
                        FormBases form =new FormBases();
                        if (form.DeleteBus(driver.DBus , context))
                        {
                            //context.Update();
                            
                            Driver driver1 = context.Drivers.Find(Id);
                            //listView2.Selected
                            //button4.PerformClick();
                            context.Drivers.Remove(driver1);
                            context.SaveChanges();
                            

                        }
                        
                        


                        //MessageBox.Show("Realy want"+dr.ToString());
                    }
                    else
                    {
                        
                        context.Drivers.Remove(driver);
                        context.SaveChanges();
                    }
                    //context.Entry(excursionType).State = EntityState.Modified;
                    //context.SaveChanges();
                    View2Refresh();
                    //listView1.Items.Clear();
                    listView3.Items.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Fail delete");
            }
        }
    }
}
