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
    public partial class Workers : Form
    {
        public Workers()
        {
            InitializeComponent();
        }

        private void Workers_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        //Dodawanie rekordów do bazy
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = @"INSERT INTO [Magazyn].[dbo].[Workers]
           ([WorkerFirstName]
           ,[WorkerLastName]
           ,[Department]
           ,[Post]
           ,[Number])
     VALUES
           ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
            // Wczytawanie bazy
            LoadData();
        }
        //Warunek do usuwania rekordów z bazy po numerze telefonu 
        private bool IfWorkerExists(SqlConnection sqlConnection1 , string Number )
        {
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT 1 FROM [Workers] WHERE [Number]='" + Number + "'", sqlConnection1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void LoadData()
        {
            SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM [Magazyn].[dbo].[Workers]", sqlConnection1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt1.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["worker_id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["WorkerFirstName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["WorkerLastName"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Department"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Post"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Number"].ToString();
            }
        }
        // Ułatwinie( do usuwania rekordów) po podwójnym kliknięciu w rekord załaduje nam dane w textboxy
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            
        }
        //Usuwanie rekordów po numerze telefonu
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            var sqlQuery = "";
            if (IfWorkerExists(sqlConnection1, textBox5.Text))
            {
                sqlConnection1.Open();
                sqlQuery = @"DELETE FROM [Workers] WHERE [Number] = '" + textBox5.Text + "'";
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
