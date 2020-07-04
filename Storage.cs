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
    public partial class Storage : Form
    {
     

        public Storage()
        {
            InitializeComponent();
        }
      

        private void Storage_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.MdiParent = this;
            pro.Show();
        }

        private void workersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workers wor = new Workers();
            wor.MdiParent = this;
            wor.Show();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients cli = new Clients();
            cli.MdiParent = this;
            cli.Show();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders ord = new Orders();
            ord.MdiParent = this;
            ord.Show();
        }

        private void ordersDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders_products ordprod = new Orders_products();
            ordprod.MdiParent = this;
            ordprod.Show();

        }
    }
}