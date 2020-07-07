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
            SharedSqlCommand sharedSqlCommand = new SharedSqlCommand();
            if (sharedSqlCommand.IfOrderExists( textBox1.Text))
            {
                MessageBox.Show("Record With this ID already exists");
            }
            else if(!sharedSqlCommand.IfClientExists(textBox2.Text))
            {
                MessageBox.Show("There is no client with this ID"); 

            }
            else if(!sharedSqlCommand.DateTimeValidation(textBox3.Text))
            {
                MessageBox.Show("Wrong date format");
            }
                else
            {
                cmd.CommandText = @"INSERT INTO [Magazyn].[dbo].[Orders]
           ([order_id]
            ,[client_id]
           ,[order_date])
     VALUES
           ('" + textBox1.Text + "','" + textBox2.Text + "','" + sharedSqlCommand.ConvertStringDateTime(textBox3.Text).ToString("MM-dd-yyyy") + "')";
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
            }
            // Wczytawanie bazy
            LoadData();
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
            SharedSqlCommand sharedSqlCommand = new SharedSqlCommand();
            if (sharedSqlCommand.IfOrderExists( textBox1.Text))
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
