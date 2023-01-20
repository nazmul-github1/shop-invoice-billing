using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Shop_Invoice_billing__system
{
    public partial class Admin_panel : Form
    {
        public Admin_panel()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=localhost; user id= root; database= shop_invoice_billing_system; password= Nazmul@1090");
            MySqlDataAdapter mda = new MySqlDataAdapter("SELECT COUNT(*) FROM admin_panel WHERE Username= '"+textBox1.Text+"' and password='"+textBox2.Text+"'",con);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                Selling_Dashboard sd = new Selling_Dashboard();
                sd.Show();
            }
            else
            {
                MessageBox.Show("Incorrect username or password!");
            }
        }

        private void Admin_panel_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
