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
    /// Klasa zawierająca metody do logowania
    /// </summary>
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        //przycisk logowania
        /// <summary>
        /// Sprawdzanie poprawności danych do logowania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM[dbo].[Login] Where UserName = '" + textBox1.Text + "' and Password = '" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //warunek sprawdzający poprawność wpisanych danych login i hasło (muszą zgadzać się z tymi w bazie)
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                Storage main = new Storage();
                main.Show();
            }
            //jeśli login i hasło się nie zgadza pojawi się powiadomienie (error) po naciśnieciu ok czyszczą się textboxy oraz focus jest na ustawiany na loginie
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
