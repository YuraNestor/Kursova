using Microsoft.EntityFrameworkCore;
using WinFormsnEntityFrameworkCoreAppKursova.Models;
using System.Linq;

namespace WinFormsnEntityFrameworkCoreAppKursova
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            View1Refresh();
            //LoadExc();
        }
        public Form1(string userRole)
        {
            InitializeComponent();
            View1Refresh();
            UserRole = userRole;
            if(UserRole != "Admin")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
            //LoadExc();
        }
        private string UserRole;
        private void LoadExc()
        {
            using (ExcursionContext excursionContext = new ExcursionContext())

            {
                View1Refresh();
                //dataGridView1.DataSource = excursionContext.Excursions.ToList();
                //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //dataGridView1.Columns["Buses"].Visible = false;

                //dataGridView1.Columns["Customer"].Visible = false;
            }
        }

        
        private decimal FullPrice(Excursion excursion, decimal priceDisel)
        {
            decimal fullPrice = excursion.Price * excursion.NumberOfTourists * excursion.Duration/60;
            foreach(Bus bus in excursion.Buses)
            {
                fullPrice += priceDisel * bus.FuelConsumption * excursion.Distance / 100;
            }
            return fullPrice;
        }
        private void View1Refresh()
        {
            
            using (ExcursionContext excursionContext = new ExcursionContext())
            {

                listView1.Items.Clear();
               // var exc = excursionContext.Excursions.ToList();
                var exxc = excursionContext.Excursions.Where(excursion => (excursion.DateOfExcursions >= dateTimePicker1.Value && excursion.DateOfExcursions <= dateTimePicker2.Value)).ToList();
                //var exxxc = exxc.S;
                foreach (var excursion in exxc)
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
        //protected string Selectedinformation;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //label1.Text = "";
            //foreach(var item in listView1.SelectedIndices)
            //    label1.Text += item.ToString();
            using (ExcursionContext excursionContext = new ExcursionContext())
            {
                if (listView1.SelectedIndices.Count == 0)
                {
                    button2.Visible = false;
                    listView2.Items.Clear();
                    return;
                }
                button2.Visible = true;
                //var item =sender as ListViewItem;
                label1.Text="Excursion info\n";
                /*Selectedinformation =*/ label1.Text+=GetInfoAboutListView(listView1);
                //int index = dataGridView1.SelectedRows[0].Index;
                //var exc = excursionContext.Excursions.ToList();
                //dataGridView2.DataSource = exc[listView1.SelectedIndices[0]].Buses.ToList();
                ViewExcursionBases(excursionContext);

            }
        }
        protected internal string GetInfoAboutListView(ListView listView)
        {
            var itemm = listView.SelectedItems[0];
            //var a = itemm.SubItems[0];
            int index = 0;
            //ListView itr=itemm.ListView;
            string info=string.Empty;
            foreach (ListViewItem.ListViewSubItem i in itemm.SubItems)
            {

                info += listView.Columns[index].Text + " " + i.Text + "\n";
                index++;
            }
            return info;
        }

        public int GetIndexById<T>(List<T> cls, InterfaceCl cl)
        {

            //if(cl.Id == Id)
            //{
            //    return Id;
            //}
            //return -1;
            //cl.
            
            int i = 0;
            //if(GetType(cl)==GetBus)
            foreach (var clas in cls)
            {
                
                if ((clas as InterfaceCl).Id == cl.Id)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        private void ViewExcursionBases(ExcursionContext excursionContext)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            int Id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            var buses = excursionContext.Excursions.Find(Id).Buses.ToList();
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
        
        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
                using (ExcursionContext context = new ExcursionContext())
                {
                    FormAddEdExcursion formAddEdExcursion = new FormAddEdExcursion(-1);
                    formAddEdExcursion.Text = (sender as Button).Text;
                    //formAddEdExcursion.Show();
                    //foreach (Bus bus in context.Buses)
                    //{
                    //    //Bus checkedBus=checkBus(bus.)
                    //    formAddEdExcursion.checkedListBox1.Items.Add(bus);
                    //}
                    if (formAddEdExcursion.ShowDialog(this) != DialogResult.Yes)
                        return;
                    //Excursion excursion=new Excursion();
                    //excursion = formAddEdExcursion.excursion1;
                    Excursion excursion = new Excursion();
                    excursion.Name = formAddEdExcursion.textBox1.Text;

                    int Id = (formAddEdExcursion.comboBox1.SelectedItem as Customer).Id;


                    excursion.ExcCustomer = context.Customers.Find(Id);
                    excursion.DateOfExcursions = formAddEdExcursion.dateTimePicker1.Value;
                    excursion.Duration = Convert.ToInt32(formAddEdExcursion.numericUpDown1.Value);
                    excursion.Destination = formAddEdExcursion.textBox2.Text;
                    excursion.Distance = formAddEdExcursion.numericUpDown2.Value;
                    excursion.NumberOfTourists = Convert.ToInt32(formAddEdExcursion.numericUpDown3.Value);
                    excursion.Price = formAddEdExcursion.numericUpDown4.Value;
                    
                    Id = (formAddEdExcursion.comboBox2.SelectedItem as ExcursionType).Id;
                    excursion.ExcType = context.ExcursionTypes.Find(Id);

                    int i = 0;

                    foreach (var item in formAddEdExcursion.checkedListBox1.Items)
                    {

                        foreach (var chitem in formAddEdExcursion.checkedListBox1.CheckedItems)
                        {
                            if (item == chitem)
                            {


                                ///int Index = GetIndexById(buses, (chitem as Bus));
                                int inu = GetIndexById(context.Buses.ToList(), (chitem as Bus));
                                excursion.Buses.Add(context.Buses.ToList()[inu]);
                            }


                        }
                        i++;
                    }
                    
                    decimal fPrice = FullPrice(excursion, 55);
                    excursion.Price = fPrice;
                    excursion.ExcCustomer.Deposit -= fPrice;
                    context.Excursions.Add(excursion);
                    context.SaveChanges();
                }
            //}
            //catch
            //{

            //}
            
            //button1.Visible = false;
            View1Refresh();
            listView2.Items.Clear();
            //formAddEdExcursion.selected = -1;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                using (ExcursionContext context = new ExcursionContext())
                {
                    FormAddEdExcursion formAddEdExcursion = new FormAddEdExcursion(-1);
                    formAddEdExcursion.Text = (sender as Button).Text;
                    //formAddEdExcursion.Show();
                    if (listView1.SelectedItems.Count == 0)
                    {
                        return;
                    }
                    //int excIndex=
                    int Id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    Excursion excursion = context.Excursions.Find(Id);
                    excursion.ExcCustomer.Deposit += excursion.Price;
                    
                    //Excursion excursion = context.Excursions.ToList()[Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text)-1];

                    formAddEdExcursion.textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                    formAddEdExcursion.comboBox1.SelectedItem = excursion.ExcCustomer;
                    bool pr = false;
                    foreach (Customer customer in formAddEdExcursion.comboBox1.Items)
                    {
                        if (customer.Id == excursion.ExcCustomer.Id)
                        {
                            formAddEdExcursion.comboBox1.SelectedIndex = formAddEdExcursion.comboBox1.Items.IndexOf(customer);
                            pr = true;
                        }
                    }
                    if (!pr)
                    {
                        formAddEdExcursion.comboBox1.Items.Add(excursion.ExcCustomer);
                        formAddEdExcursion.comboBox1.SelectedIndex = formAddEdExcursion.comboBox1.Items.IndexOf(excursion.ExcCustomer);
                    }
                    formAddEdExcursion.comboBox1.SelectedIndex = GetIndexById<Customer>(context.Customers.ToList(), excursion.ExcCustomer);
                    formAddEdExcursion.dateTimePicker1.Value = Convert.ToDateTime(listView1.SelectedItems[0].SubItems[3].Text);
                    formAddEdExcursion.numericUpDown1.Value = Convert.ToDecimal(listView1.SelectedItems[0].SubItems[4].Text);
                    formAddEdExcursion.textBox2.Text = Convert.ToString(listView1.SelectedItems[0].SubItems[5].Text);
                    formAddEdExcursion.numericUpDown2.Value = Convert.ToDecimal(listView1.SelectedItems[0].SubItems[6].Text);
                    formAddEdExcursion.numericUpDown3.Value = Convert.ToDecimal(listView1.SelectedItems[0].SubItems[7].Text);
                    formAddEdExcursion.numericUpDown4.Value = Convert.ToDecimal(listView1.SelectedItems[0].SubItems[8].Text);
                    formAddEdExcursion.comboBox2.SelectedIndex = GetIndexById<ExcursionType>(context.ExcursionTypes.ToList(), excursion.ExcType);
                    var busess = excursion.Buses;
                    formAddEdExcursion.excursion1 = excursion;
                    foreach (Bus bus in context.Buses)
                    {
                        bool flag = false;
                        foreach (Bus excBus in busess)
                        {

                            if (excBus.Id == bus.Id)
                            {
                                flag = true;
                                break;
                            }


                        }

                        if(flag)
                            formAddEdExcursion.checkedListBox1.Items.Add(bus, true);




                    }

                    if (formAddEdExcursion.ShowDialog(this) != DialogResult.OK)
                        return;
                    //Excursion excursion=new Excursion();
                    //excursion = formAddEdExcursion.excursion1;
                    //Excursion excursion = new Excursion();
                    excursion.Name = formAddEdExcursion.textBox1.Text;


                    excursion.ExcCustomer = context.Customers.ToList()[formAddEdExcursion.comboBox1.SelectedIndex];
                    excursion.DateOfExcursions = formAddEdExcursion.dateTimePicker1.Value;
                    excursion.Duration = Convert.ToInt32(formAddEdExcursion.numericUpDown1.Value);
                    excursion.Destination = formAddEdExcursion.textBox2.Text;
                    excursion.Distance = formAddEdExcursion.numericUpDown2.Value;
                    excursion.NumberOfTourists = Convert.ToInt32(formAddEdExcursion.numericUpDown3.Value);
                    excursion.Price = formAddEdExcursion.numericUpDown4.Value;
                    excursion.ExcType = context.ExcursionTypes.ToList()[formAddEdExcursion.comboBox2.SelectedIndex];

                    int i = 0;
                    excursion.Buses.Clear();
                    foreach (var item in formAddEdExcursion.checkedListBox1.Items)
                    {

                        foreach (var chitem in formAddEdExcursion.checkedListBox1.CheckedItems)
                        {
                            if (item == chitem)
                            {


                                ///int Index = GetIndexById(buses, (chitem as Bus));
                                int inu = GetIndexById(context.Buses.ToList(), (chitem as Bus));
                                excursion.Buses.Add(context.Buses.ToList()[inu]);
                            }


                        }
                        i++;
                    }
                    excursion.ExcCustomer.Deposit -= excursion.Price;
                    context.Entry(excursion).State = EntityState.Modified;
                    context.SaveChanges();
                    ViewExcursionBases(context);
                }
            }
            catch
            {

            }
            
            //button1.Visible = false;
            View1Refresh();
            listView2.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try{
                using (ExcursionContext context = new ExcursionContext())
                {
                    int Id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    Excursion excursion = context.Excursions.Find(Id);
                    var dr = MessageBox.Show("Return Customer's money ", "Confirmation", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No || dr == DialogResult.OK)
                    {
                        
                    }
                    else
                    {
                        excursion.ExcCustomer.Deposit+=excursion.Price;
                    }   
                    context.Excursions.Remove(excursion);
                    context.SaveChanges();
                    View1Refresh();
                    listView2.Items.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Choose Element!!!");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormBases formBases = new FormBases(UserRole);
            if (formBases.ShowDialog(this) == DialogResult.Cancel)
                return;
            View1Refresh();
            listView2.Items.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormCustomers formCustomers = new FormCustomers(UserRole);
            if (formCustomers.ShowDialog(this) == DialogResult.Cancel)
                return;
            View1Refresh();
            listView2.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormTypes formTypes = new FormTypes(UserRole);
            if (formTypes.ShowDialog(this) == DialogResult.Cancel)
                return;
            View1Refresh();
            listView2.Items.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormDrivers formDrivers = new FormDrivers(UserRole);
            if (formDrivers.ShowDialog(this) == DialogResult.Cancel)
                return;
            listView2.Items.Clear();
            View1Refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                var dt= dateTimePicker1.Value;
                dateTimePicker2.Value = dt.AddDays(1);
            }
            listView2.Items.Clear();
            View1Refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormZvit formZvit= new FormZvit();
            formZvit.ShowDialog(this);
        }
    }
}