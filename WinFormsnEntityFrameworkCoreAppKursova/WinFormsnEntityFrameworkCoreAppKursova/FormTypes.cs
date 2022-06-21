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
    public partial class FormTypes : Form
    {
        public FormTypes()
        {
            InitializeComponent();
            View2Refresh();
        }
        public FormTypes(string userRole)
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
                var excTypes = context.ExcursionTypes.ToList();
                foreach (var excType in excTypes)
                {

                    ListViewItem item = new ListViewItem(excType.Id.ToString());

                    item.SubItems.Add(excType.Name);
                    



                    listView2.Items.Add(item);



                }
            }
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedIndices.Count == 0)
            {
                button3.Visible=button4.Visible = false;
                listView1.Items.Clear();
                return;
            }
            using (ExcursionContext excursionContext = new ExcursionContext())
            {
                button3.Visible=button4.Visible = true;
                try
                {

                    listView1.Items.Clear();
                    var exc = excursionContext.ExcursionTypes.ToList()[listView2.SelectedIndices[0]].Excursions.ToList();
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
                catch
                {
                    listView1.Items.Clear();
                }
                listView3.Items.Clear();
                var buses = excursionContext.ExcursionTypes.ToList()[listView2.SelectedIndices[0]].Buses.ToList();
                try
                {
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
                            types += type.ToString()+", ";
                        }
                        item.SubItems.Add(types);

                        listView3.Items.Add(item);



                    }
                }
                catch
                {
                    listView3.Items.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ExcursionContext context = new ExcursionContext())
            {
                FormAddOrEditOther formAddType=new FormAddOrEditOther();
                formAddType.Text = (sender as Button).Text;
                if (formAddType.ShowDialog(this) != DialogResult.Yes)
                    return;
                ExcursionType excursionType = new ExcursionType();
                excursionType.Name=formAddType.textBoxName.Text;
                context.ExcursionTypes.Add(excursionType);
                context.SaveChanges();
                View2Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (ExcursionContext context = new ExcursionContext())
                {
                    FormAddOrEditOther formAddType = new FormAddOrEditOther();
                    formAddType.Text = (sender as Button).Text;
                    int Id = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                    ExcursionType excursionType = context.ExcursionTypes.Find(Id);
                    if(excursionType.Excursions.Count!=0 || excursionType.Buses.Count != 0)
                    {
                        var dr = MessageBox.Show("If you change this type you will make changes to the existing data", "Edit", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No || dr == DialogResult.OK)
                            return;
                    }
                    formAddType.textBoxName.Text=excursionType.Name;
                    if (formAddType.ShowDialog(this) != DialogResult.Yes)
                        return;
                    
                    excursionType.Name = formAddType.textBoxName.Text;
                    context.Entry(excursionType).State = EntityState.Modified;
                    context.SaveChanges();
                    View2Refresh();
                }
            }
            catch
            {
                MessageBox.Show("Fail edit choose element!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (ExcursionContext context = new ExcursionContext())
                {
                   
                    int Id = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                    ExcursionType excursionType = context.ExcursionTypes.Find(Id);
                    if (excursionType.Excursions.Count != 0 || excursionType.Buses.Count != 0)
                    {
                        List<Bus> oneTypeBuses = new List<Bus>();
                        string strBuses= "\n";
                        foreach(Bus bus in excursionType.Buses)
                        {
                            if (bus.ExcursionTypes.Count == 1)
                            {
                                strBuses += bus.ToString() + " ";
                                oneTypeBuses.Add(bus);
                            }
                            
                        }
                        string strExcursions = "\n"+ excursionType.Excursions.Count.ToString()+"Excursions: ";
                        foreach (Excursion excursion in excursionType.Excursions)
                        {
                            strExcursions += excursion.ToString() + " ";
                        }
                        var dr = MessageBox.Show("If delete this type to be removed"+oneTypeBuses.Count.ToString() + " Buses: " + strBuses + strExcursions, "Confirmation", MessageBoxButtons.YesNo);
                        if ( dr == DialogResult.No ||dr == DialogResult.OK)
                            return;
                        context.Excursions.RemoveRange(excursionType.Excursions);
                        
                        //foreach(Bus bus in excursionType.Buses)
                        //{
                        //    FormBases formBases = new FormBases();
                        //    if (!formBases.DeleteBus(bus))
                        //    {
                        //        return;
                        //    }
                        //}
                        context.Buses.RemoveRange(oneTypeBuses);
                        context.ExcursionTypes.Remove(excursionType);
                        context.SaveChanges();
                        
                        
                        //MessageBox.Show("Realy want"+dr.ToString());
                    }
                    else
                    {
                        context.ExcursionTypes.Remove(excursionType);
                        context.SaveChanges();
                    }
                    //context.Entry(excursionType).State = EntityState.Modified;
                    //context.SaveChanges();
                    View2Refresh();
                    listView1.Items.Clear();
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
