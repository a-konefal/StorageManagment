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
    public partial class Orders_products : Form
    {
        public Orders_products()
        {
            InitializeComponent();
        }

        private void Orders_products_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SharedSqlCommand sharedSqlCommand = new SharedSqlCommand();
            if (!sharedSqlCommand.IfProductsExists(textBox2.Text) || !sharedSqlCommand.IfOrderExists(textBox1.Text))
            {
                MessageBox.Show("Order or Product not exists");

            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = @"INSERT INTO [Magazyn].[dbo].[Orders_products]
           ([order_id]
           ,[ProductCode])
          
                VALUES
           
           ('" + textBox1.Text + "','" + textBox2.Text + "')";
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                // Wczytawanie bazy
                LoadData();
            }
        }
        //Warunek do usuwania rekordów(całych zamówień) z bazy po numerze zamówienia
        private bool IfOrdprodExists(SqlConnection sqlConnection1, string order_id)
        {
            SqlDataAdapter sda4 = new SqlDataAdapter("SELECT 1 FROM [Orders_products] WHERE [order_id]='" + order_id + "'", sqlConnection1);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            if (dt4.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void LoadData()
        {
            SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlDataAdapter sda4 = new SqlDataAdapter("SELECT * FROM [Magazyn].[dbo].[Orders_products]", sqlConnection1);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt4.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ord_prod_id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["order_id"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["ProductCode"].ToString();
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            var sqlQuery = "";
            if (IfOrdprodExists(sqlConnection1, textBox1.Text))
            {
                sqlConnection1.Open();
                sqlQuery = @"DELETE FROM [Orders_products] WHERE [order_id] = '" + textBox1.Text + "'";
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
