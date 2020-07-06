using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMagazine
{
    public class SharedSqlCommand
    {
        public SqlConnection SqlCon { get; private set; }
        public SharedSqlCommand()
        {
            SqlCon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
        }

        public bool IfProductsExists( string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT 1 FROM [Products] WHERE [ProductCode]='" + productCode + "'", SqlCon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public bool IfOrderExists( string order_id)
        {
            SqlDataAdapter sda3 = new SqlDataAdapter("SELECT 1 FROM [Orders] WHERE [order_id]='" + order_id + "'", SqlCon);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            if (dt3.Rows.Count > 0)
                return true;
            else
                return false;
        }

    }
}
