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
    /// <summary>
    /// Klasa zawiera metody pozwalające na dodawanie rekordów do bazy danych z wartości wprowadzonych przez użytkownika
    /// </summary>
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }
        // // ładowanie zamówień w datagridzie
        private void Orders_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        // przycisk add 
        /// <summary>
        /// Dodawanie rekordów do bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            SharedSqlCommand sharedSqlCommand = new SharedSqlCommand();
            // sprawdzenie czy nie ma już zamówienia o takim id 
            if (sharedSqlCommand.IfOrderExists( textBox1.Text))
            {
                MessageBox.Show("Record With this ID already exists");
            }
            // sprawdzenie czy klient jest w bazie
            else if(!sharedSqlCommand.IfClientExists(textBox2.Text))
            {
                MessageBox.Show("There is no client with this ID"); 

            }
            // sprawdzenie formatu daty
            else if(!sharedSqlCommand.DateTimeValidation(textBox3.Text))
            {
                MessageBox.Show("Wrong date format");
            }
            // jeśli wszystko powyżej się zgadza, dodaje rekord do tabeli Orders
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
        // metoda zaczytująca rekordy z bazy do datagridview
        /// <summary>
        /// Metoda wczytywania rekordów do datagridview
        /// </summary>
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
        //usuwanie rekordów (po order_id)
        /// <summary>
        /// Usuwanie rekordów z bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
