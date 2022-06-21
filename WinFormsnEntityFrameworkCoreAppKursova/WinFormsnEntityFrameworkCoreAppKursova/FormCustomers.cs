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
    public partial class FormCustomers : Form
    {
        public FormCustomers()
        {
            InitializeComponent();
            View2Refresh();
        }
        public FormCustomers(string userRole)
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
                var customers = context.Customers.ToList();
                foreach (var customer in customers)
                {

                    ListViewItem item = new ListViewItem(customer.Id.ToString());

                    item.SubItems.Add(customer.Name);
                    item.SubItems.Add(customer.Deposit.ToString());
                    
                    

                    listView2.Items.Add(item);



                }
            }
        }
        
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedIndices.Count == 0)
            {
                //button2.Visible = false;
                listView1.Items.Clear();
                return;
            }
            using (ExcursionContext excursionContext = new ExcursionContext())
            {
                try
                {
                    listView1.Items.Clear();
                    var exc = excursionContext.Customers.ToList()[listView2.SelectedIndices[0]].Excursions.ToList();
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
                
            }
        }

        private void button2_Click(object sender, EventArgs e) //add customer
        {
            using (ExcursionContext context = new ExcursionContext())
            {
                FormAddOrEditOther formAddCustomer = new FormAddOrEditOther();
                formAddCustomer.Text = (sender as Button).Text;
                Customer customer = new Customer();
                

                NumericUpDown numericUpDownDeposit = new NumericUpDown();
                numericUpDownDeposit.Location = new System.Drawing.Point(24, 81);
                numericUpDownDeposit.Maximum = 100000;
                numericUpDownDeposit.Minimum = -10000;
                numericUpDownDeposit.Increment = 50;
                numericUpDownDeposit.DecimalPlaces = 2;
                Label label = new Label();
                label.Text = "Deposite";
                label.Location = new System.Drawing.Point(150, 81);

                formAddCustomer.Controls.Add(numericUpDownDeposit);
                formAddCustomer.Controls.Add(label);

                if (formAddCustomer.ShowDialog(this) != DialogResult.Yes)
                    return;
                customer.Name = formAddCustomer.textBoxName.Text;
                customer.Deposit=numericUpDownDeposit.Value;
                context.Customers.Add(customer);
                context.SaveChanges();
                View2Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e) //edit customer
        {
            try
            {
                if (listView2.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Choose customer!");
                    return;
                }
                using (ExcursionContext context = new ExcursionContext())
                {

                    FormAddOrEditOther formEditCustomer = new FormAddOrEditOther();
                    formEditCustomer.Text = (sender as Button).Text;
                    int Id = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                    Customer customer = context.Customers.Find(Id);
                    if (customer.Excursions.Count != 0)
                    {
                        var dr = MessageBox.Show("If you change this customer you will make changes to the existing data", "Edit", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No || dr == DialogResult.OK)
                            return;
                    }
                    formEditCustomer.textBoxName.Text = customer.Name;

                    NumericUpDown numericUpDownDeposit = new NumericUpDown();
                    numericUpDownDeposit.Location = new System.Drawing.Point(24, 81);
                    numericUpDownDeposit.Maximum = 100000;
                    numericUpDownDeposit.Minimum = -10000;
                    numericUpDownDeposit.Increment = 50;
                    numericUpDownDeposit.DecimalPlaces = 2;

                    numericUpDownDeposit.Value=customer.Deposit;
                    
                    Label label = new Label();
                    label.Text = "Deposite";
                    label.Location = new System.Drawing.Point(150, 81);
                    formEditCustomer.Controls.Add(numericUpDownDeposit);
                    formEditCustomer.Controls.Add(label);   
                    if (formEditCustomer.ShowDialog(this) != DialogResult.Yes)
                        return;
                    customer.Name = formEditCustomer.textBoxName.Text;
                    customer.Deposit = numericUpDownDeposit.Value;
                    context.Entry(customer).State = EntityState.Modified;
                    context.SaveChanges();
                    View2Refresh();
                }
            }
            catch
            {
                MessageBox.Show("Choose customer!!");
            }
        }

        private void button4_Click(object sender, EventArgs e) //delete
        {
            try
            {
                using (ExcursionContext context = new ExcursionContext())
                {
                    if (listView2.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Choose customer!");
                        return;
                    }

                    int Id = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                    Customer customer = context.Customers.Find(Id);
                    if (customer.Excursions.Count != 0)
                    {
                        
                        string strExcursions = "\n" + customer.Excursions.Count.ToString() + "Excursions: ";
                        foreach (Excursion excursion in customer.Excursions)
                        {
                            strExcursions += excursion.ToString() + " ";
                        }
                        var dr = MessageBox.Show("If delete this type to be removed" +  strExcursions, "Confirmation", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No || dr == DialogResult.OK)
                            return;
                        context.Excursions.RemoveRange(customer.Excursions);
                        
                        context.Customers.Remove(customer);
                        context.SaveChanges();


                        //MessageBox.Show("Realy want"+dr.ToString());
                    }
                    else
                    {
                        context.Customers.Remove(customer);
                        context.SaveChanges();
                    }
                    //context.Entry(excursionType).State = EntityState.Modified;
                    //context.SaveChanges();
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
