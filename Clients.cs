using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorageMagazine
{
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            if (!IsStringInvalid(textBox8.Text))
            {
                MessageBox.Show("Nip contain to many numbers there must be a 13.");

            }
            else {
                cmd.CommandText = @"INSERT INTO [Magazyn].[dbo].[Clients]
           ([FirstName]
           ,[LastName]
           ,[Company]
           ,[Street]
           ,[Number]
           ,[City]
           ,[PostalCode]
           ,[NIP]
           ,[Telephone])
     VALUES
           
           ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','"
               + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "')";
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                // Wczytawanie bazy
                LoadData();
            }
        }
       
        
        public void LoadData()
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlDataAdapter sda2 = new SqlDataAdapter("SELECT * FROM [Magazyn].[dbo].[Clients]", sqlConnection1);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt2.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["client_id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["FirstName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["LastName"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Company"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Street"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Number"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["City"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["PostalCode"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["NIP"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = item["Telephone"].ToString();
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            textBox9.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            var sqlQuery = "";
            SharedSqlCommand sharedSqlCommand = new SharedSqlCommand();
            if (sharedSqlCommand.IfClientExists(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
            {
                sqlConnection1.Open();
                sqlQuery = $@"DELETE FROM [Clients] WHERE [client_id] = {dataGridView1.SelectedRows[0].Cells[0].Value.ToString()}";
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
        public bool IsStringInvalid(string text)
        {
            return string.IsNullOrEmpty(text) ||
                text.Length <= 13 ||
                !Regex.IsMatch(text, textBox8.Text);
        }
    }
}
