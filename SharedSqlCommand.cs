using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public bool IfClientExists(string clientId)
        {
            SqlDataAdapter sda2 = new SqlDataAdapter("SELECT 1 FROM [Clients] WHERE [client_id]='" + clientId + "'", SqlCon);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            if (dt2.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public bool IfWorkerExists(string WorkerFirstName, string WorkerLastName)
        {
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT 1 FROM [Workers] WHERE [WorkerFirstName]='" + WorkerFirstName + "'AND [WorkerLastName]='" + WorkerLastName + "'", SqlCon);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public bool DateTimeValidation(string orderDate) 
        {
            DateTime data1 = DateTime.Now;
            bool isValid =  DateTime.TryParse(orderDate, out data1);
            return isValid;
        }
        public DateTime ConvertStringDateTime(string orderDate)
        {
            DateTime data1 = DateTime.Now;
            DateTime.TryParse(orderDate, out data1);
            return data1;
        }
        public bool QuantityValidation(string text)
        {
            Regex _regex = new Regex("[^0-9.-]+");
            return !_regex.IsMatch(text);
        }
    }
}
