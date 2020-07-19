using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorageMagazine
{
    /// <summary>
    /// Klasa zawiera metody pozwalające na dodawanie i usuwanie rekordów do bazy danych z wartości wprowadzonych przez użytkownika
    /// </summary>
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Metoda wczytująca bazę danych do datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }
        /// <summary>
        /// Metoda dodawania i updatowania rekordów do bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            con.Open();
            bool status = false;
            if (comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            var sqlQuery = "";
            SharedSqlCommand sharedSqlCommand = new SharedSqlCommand();
            if (!sharedSqlCommand.QuantityValidation(textBox3.Text))
            {
                MessageBox.Show("Quantity must be a number");
            }
            else if (sharedSqlCommand.IfProductsExists( textBox1.Text))
            {
                sqlQuery = @"UPDATE [Products] SET [Quantity] = '" + textBox3.Text + "' ,[ProductStatus] = '" + status + "'  WHERE [ProductCode] = '" + textBox1.Text + "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO [Magazyn].[dbo].[Products] ([ProductCode] ,[ProductName] ,[Quantity] ,[ProductStatus]) VALUES
                                ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + status + "')";
            }
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();
            // Wczytawanie bazy
            LoadData();
            ResetRecords();

           
        }
        //Update rekordów
        /// <summary>
        /// Metoda wczytująca bazę danych do datagridview
        /// </summary>
        public void LoadData()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Magazyn].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Quantity"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[3].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[3].Value = "Inactive";
                }

            }

        }
        /// <summary>
        /// Metoda zaczytująca dane z datagridview do textboxów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1.Text = "Update";
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
           
        }
        //Usuwanie rekordu
        /// <summary>
        /// Metoda do usuwania rekordów z bazy po product codzie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            var sqlQuery = "";
            SharedSqlCommand sharedSqlCommand = new SharedSqlCommand();
            if (sharedSqlCommand.IfProductsExists(textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Products] WHERE [ProductCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record Not Exists");
            }
            // Wczytawanie bazy
            LoadData();
        }
        /// <summary>
        /// Zamiana przysku update na add
        /// </summary>
        private void ResetRecords()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            button1.Text = "Add";
            textBox1.Focus();
        }
    }
}
