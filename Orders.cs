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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            if (IfOrderExists(sqlConnection1, textBox1.Text))
            {
                MessageBox.Show("Record With this ID already exists");
            }
            else
            {
                cmd.CommandText = @"INSERT INTO [Magazyn].[dbo].[Orders]
           ([order_id]
            ,[client_id]
           ,[order_date])
     VALUES
           ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
            }
            // Wczytawanie bazy
            LoadData();
        }
        private bool IfOrderExists(SqlConnection sqlConnection1, string order_id)
        {
            SqlDataAdapter sda3 = new SqlDataAdapter("SELECT 1 FROM [Orders] WHERE [order_id]='" + order_id + "'", sqlConnection1);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            if (dt3.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void LoadData()
        {
            SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM [Magazyn].[dbo].[Orders]", sqlConnection1);
            DataTable dt3 = new DataTable();
            sda1.Fill(dt3);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt3.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["order_id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["client_id"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["order_date"].ToString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            var sqlQuery = "";
            if (IfOrderExists(sqlConnection1, textBox1.Text))
            {
                sqlConnection1.Open();
                sqlQuery = @"DELETE FROM [Orders] WHERE [order_id] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, sqlConnection1);
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
            }
            else
            {
                MessageBox.Show("Record Not Exists");
            }
            // Wczytawanie bazy
            LoadData();
        }
    }
}
