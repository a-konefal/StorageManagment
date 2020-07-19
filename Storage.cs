using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorageMagazine
{
    /// <summary>
    /// Klasa programu
    /// </summary>
    public partial class Storage : Form
    {
     

        public Storage()
        {
            InitializeComponent();
        }
      
        /// <summary>
        /// Po zamknięciu okna wyłącza program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Storage_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Metoda wyświetlająca formularz produktów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.MdiParent = this;
            pro.Show();
        }
        /// <summary>
        /// Metoda wyświetlająca formularz pracowników
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workers wor = new Workers();
            wor.MdiParent = this;
            wor.Show();
        }
        /// <summary>
        /// Metoda wyświetlająca formularz klientów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients cli = new Clients();
            cli.MdiParent = this;
            cli.Show();
        }
        /// <summary>
        /// Metoda wyświetlająca formularz zamówień
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders ord = new Orders();
            ord.MdiParent = this;
            ord.Show();
        }
        /// <summary>
        /// Metoda wyświetlająca formularz szczegółów zamówień
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ordersDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders_products ordprod = new Orders_products();
            ordprod.MdiParent = this;
            ordprod.Show();

        }
    }
}