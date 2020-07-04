using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorageMagazine
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM[dbo].[Login] Where UserName = '" + textBox1.Text + "' and Password = '" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                Storage main = new Storage();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid UserName or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox2.Clear();
                textBox1.Focus();
            }
        }
    }
}
