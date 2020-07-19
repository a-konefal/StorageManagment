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
    public partial class Orders_products : Form
    {
        public Orders_products()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Metoda wczytująca po włączeniu okna rekordy z bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Orders_products_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        /// <summary>
        /// Dodawanie rekordów do bazy danych
        /// </summary>
        /// <param name="sender">wysyłanie</param>
        /// <param name="e">po wcisnieciu przicisku myszy</param>
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SharedSqlCommand sharedSqlCommand = new SharedSqlCommand();
            // warunek sprawdzajacy czy produkt o podanym kodzie i zamówienie o podanym ID istnieje jeśli nie wyskoczy powiadomienie
            if (!sharedSqlCommand.IfProductsExists(textBox2.Text) || !sharedSqlCommand.IfOrderExists(textBox1.Text))
            {
                MessageBox.Show("Order or Product not exists");

            }
            // jeśli istnieją dodaje rekord do bazy
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
        /// <summary>
        /// Warunek do usuwania rekordów(całych zamówień) z bazy po numerze zamówienia
        /// </summary>
        /// <param name="sqlConnection1">conectionstring</param>
        /// <param name="order_id">id zamóweinia</param>
        /// <returns>true lub false</returns>
        
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
        /// <summary>
        /// ładowanie rekordów do datagridview
        /// </summary>
        
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
        // po podwójnym kliknieciu w rekord wyświeli nam order_id(ułatwienie do usuwania) w texboxie
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
        /// <summary>
        /// usuwanie po order_id wszystkich rekordów z podanym order_id
        /// </summary>
        /// <param name="sender">wysyłanie</param>
        /// <param name="e">wcisniecie przycisku myszy</param>
        
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
            //jeśli rekord o podanym order_id nie istnieje pokaże nam komunikat
            else
            {
                MessageBox.Show("Record Not Exists");
            }
            // Wczytawanie bazy
            LoadData();
        }
    }
}
