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
    /// Klasa pracowników
    /// </summary>
    public partial class Workers : Form
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public SqlConnection SqlCon { get; private set; }
        /// <summary>
        /// PO włączeniu forma łączy się z serwerem sql i ładuje rekordy z bazy danych
        /// </summary>
        public Workers()
            //ładowanie bazy przy włączeniu forma workers
        {
            InitializeComponent();
            SqlCon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
        }
        /// <summary>
        /// Ładowanie rekordów z bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Workers_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        //Dodawanie rekordów do bazy
        /// <summary>
        /// Metoda dodawania rekordów do bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
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
            cmd.Connection = SqlCon;
            SqlCon.Open();
            cmd.ExecuteNonQuery();
            SqlCon.Close();
            // Wczytawanie bazy
            LoadData();
        }
        //Warunek do usuwania rekordów z bazy po imieniu i nazwisku 
        /// <summary>
        /// Metoda sprawdzająca istnienie pracownika po parametrach imienia i nazwiska  (<paramref name="WorkerFirstName"/>, <paramref name="WorkerFirstName"/>)
        /// </summary>
        /// <param name="WorkerFirstName">Imię pracownika</param>
        /// <param name="WorkerLastName">Nazwisko pracownika</param>
        /// <returns>prawda lub fałsz</returns>
        private bool IfWorkerExists( string WorkerFirstName, string WorkerLastName )
        {
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT 1 FROM [Workers] WHERE [WorkerFirstName]='" + WorkerFirstName + "'AND [WorkerLastName]='" + WorkerLastName + "'", SqlCon);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
                return true;
            else
                return false;
        }
        // wyświetlanie tabeli workers w datagridview
        /// <summary>
        /// ładowanie rekordów z bazy danych do datagridview
        /// </summary>
        public void LoadData()
        {
            
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT * FROM [Magazyn].[dbo].[Workers]", SqlCon);
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
        // Ułatwinie po podwójnym kliknięciu w rekord załaduje nam dane w textboxy
        /// <summary>
        /// Metoda pozwalająca po doubleclicku w datagridview na załadowanie informacji z bazy do textboxów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            
        }
        //Usuwanie rekordów po zaznaczeniu w datagridzie (po id)
        /// <summary>
        /// Metoda usuwania rekordów z bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (IfWorkerExists(textBox1.Text, textBox2.Text))
            {
                var sqlQuery = "";
                SqlCon.Open();
                sqlQuery = $@"DELETE FROM [Workers] WHERE [worker_id] = {dataGridView1.SelectedRows[0].Cells[0].Value} "; 
                SqlCommand cmd = new SqlCommand(sqlQuery, SqlCon);
                cmd.ExecuteNonQuery();
                SqlCon.Close();
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
