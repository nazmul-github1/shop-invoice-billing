using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Grpc.Core;
using System.IO; 
using System.Diagnostics;
using System.Web;

namespace Shop_Invoice_billing__system
{
    public partial class Selling_Dashboard : Form
    {
        DataTable dt { get; set; }
        DataTable dt2 { get; set; }
        DataTable dt3 { get; set; } 
        public Selling_Dashboard()
        {
            InitializeComponent();
            string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlqueryproduct = "select * from product_name";
            MySqlCommand sqlcomm = new MySqlCommand(sqlqueryproduct, sqlconn);
            sqlconn.Open();
            MySqlDataAdapter sdr = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "product_name";
            comboBox1.ValueMember = "product_id";
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            formLoaded = false;
              
        }
        public bool formLoaded { get; set; }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formLoaded == false) { formLoaded =true;return; }

            string prodSelected = comboBox1.SelectedValue.ToString();
            string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string sqlqueryquantity = "select * from quantity where product_id=@product_id";
            MySqlCommand sqlcomm = new MySqlCommand(sqlqueryquantity, sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@product_id", prodSelected);
            MySqlDataAdapter sdr = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "quantity_name";
            comboBox2.ValueMember = "quantity_id";
            comboBox2.Enabled = true;
            comboBox3.Enabled = false;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
                string val2 = comboBox2.SelectedValue.ToString();
                string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
                MySqlConnection sqlconn = new MySqlConnection(mainconn);
                string sqlqueryamount = "select * from amount where amount_id=@quantity_id ";
                MySqlCommand sqlcomm = new MySqlCommand(sqlqueryamount, sqlconn);
                sqlconn.Open();
                sqlcomm.Parameters.AddWithValue("@quantity_id", val2);
              //  sqlcomm.Parameters.AddWithValue("@prod", comboBox1.SelectedValue.ToString());
                MySqlDataAdapter sdr = new MySqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "amount";
                comboBox3.ValueMember = "amount_id";
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
            }
            private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Selling_Dashboard_Load(object sender, EventArgs e)
        {
           // dataGridView1.AutoGenerateColumns = false;
            dt = new DataTable();
            dt2 = new DataTable(); dt3 = new DataTable();
            dt.Columns.Add("Bill no");
            dt.Columns.Add("Product name");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Amount");
            dt.Columns.Add("BILL DATE");

            dt2.Columns.Add("Client Name");
            dt2.Columns.Add("Phone Number");
            dt2.Columns.Add("Home Address"); 
            dt2.Columns.Add("E-mail Address");

            dt3.Columns.Add("Client Name");
            dt3.Columns.Add("Phone Number");
            dt3.Columns.Add("Home Address");
            dt3.Columns.Add("E-mail Address");
            dt3.Columns.Add("Total Bill Amount");
            dt3.Columns.Add("Discount");  
            dt3.Columns.Add("Net price");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=localhost; user id= root; database= shop_invoice_billing_system; password= Nazmul@1090");
            MySqlCommand cmd = new MySqlCommand("Insert Into client_info(client_name, phone_no, home_address, email_address) Values('" + textBox2.Text + "', '" + textBox4.Text + "','" + textBox6.Text + "','" + textBox1.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            // ExportToPdf(dt, @"U:\test.pdf");


            foreach (DataRow rw in dt2.Rows)
            {
                var row = dt3.NewRow();
                row["Client Name"] = rw["Client Name"].ToString();
                row["Phone Number"] = rw["Phone Number"].ToString();
                row["Home Address"] = rw["Home Address"].ToString();
                row["E-mail Address"] = rw["E-mail Address"].ToString();
                row["E-mail Address"] = textBox1.Text.ToString();
                row["Total Bill Amount"] = textBox7.Text.ToString();
                row["Discount"] = textBox9.Text.ToString();
                row["Net price"] = textBox10.Text.ToString();
                dt3.Rows.Add(row);
            }

            DataTable res = dt3;
            DataTable res2 = dt;
            PDFVeiwer frm1 = new PDFVeiwer("invoice.rpt", @"C:\Users\wH8UICTCPC\Desktop\Shop invoice\Shop Invoice billing  system\RPT", dt3, dt);
            frm1.ShowDialog();
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            var row = dt.NewRow();

            row["Bill no"] = textBox3.Text.ToString();
            row["Product name"] = comboBox1.Text.ToString();
            row["Quantity"] = comboBox2.Text.ToString();
            row["Amount"] = comboBox3.Text.ToString();
            row["BILL DATE"] = dateTimePicker1.Value.ToString();
            dt.Rows.Add(row); 
            dataGridView1.DataSource = dt;

            double sum = 0; 
            foreach (DataRow rw in dt.Rows)
            {
                sum += double.Parse(rw["Amount"].ToString());
            }

            textBox7.Text = sum.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dt.Clear();
            dataGridView1.DataSource = dt;
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            double bill =Convert.ToDouble(textBox7.Text.ToString());
            double discount = Convert.ToDouble(textBox9.Text.ToString());

            textBox10.Text = (bill - (bill * discount / 100)).ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection mcon = new MySqlConnection("server=localhost;user id=root;database= shop_invoice_billing_system ;password=Nazmul@1090");
            MySqlCommand mcmd = new MySqlCommand(@"INSERT INTO client_info(bill_no,date,client_name,phone_number,home_address,e_mail_address,product_name,quantity,amount,total_bill_amount,discount,net_price)
            VALUES('" + textBox3.Text + "', '" + dateTimePicker1.Text + "', '" + textBox2.Text + "', '" + textBox4.Text + "', '" + textBox6.Text + "', '" + textBox1.Text + "', '" + comboBox1.Text + "', '" + comboBox2.Text + "', '" + comboBox3.Text + "', '" + textBox7.Text + "', '" + textBox9.Text + "', '" + textBox10.Text + "')", mcon);
            mcon.Open();
            mcmd.ExecuteNonQuery();
            mcon.Close();
            MessageBox.Show(" Save Successful");

            var row = dt2.NewRow();
            row["Client Name"] = textBox2.Text.ToString();
            row["Phone Number"] = textBox4.Text.ToString();
            row["Home Address"] = textBox6.Text.ToString();
            row["E-mail Address"] = textBox1.Text.ToString();
            dt2.Rows.Add(row);

            MessageBox.Show("Successful");
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
