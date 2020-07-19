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
    /// <summary>
    /// Publiczna klasa do connection stringa i warunków
    /// </summary>
    public class SharedSqlCommand
    {
       
        //  metoda do connection stringa
        public SqlConnection SqlCon { get; private set; }
        public SharedSqlCommand()
        {
            SqlCon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Magazyn;Integrated Security=True");
        }
        /// <summary>
        ///  metoda z warunkiem sprawdzająca istnienie rekordu w tabeli products po productcodzie (<paramref name="productCode"/>)
        /// </summary>
        /// <param name="productCode"> kod produktu</param>
        /// <returns>prawda lub fałsz</returns>
 
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
        /// <summary>
        /// metoda z warunkiem sprawdzająca istnienie rekordu w tabeli orders po order_id (<paramref name="order_id"/>)
        /// </summary>
        /// <param name="order_id">id zamówienia</param>
        /// <returns>prawda lub fałsz</returns>
    
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
        /// <summary>
        /// metoda z warunkiem sprawdzająca istanitnie rekordu w tabeli clients po clinet_id (<paramref name="clientId"/>)
        /// </summary>
        /// <param name="clientId">id klineta</param>
        /// <returns>prawda lub fałsz</returns>
 
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
        /// <summary>
        /// metoda z warunkiem sprawdzajaca istnienie rekordu w tabeli workers po workerfirstname i workerlastname (<paramref name="WorkerFirstName"/>, <paramref name="WorkerFirstName"/>)
        /// </summary>
        /// <param name="WorkerFirstName">imię pracownika</param>
        /// <param name="WorkerLastName">nazwisko pracownika</param>
        /// <returns>prawda lub fałsz</returns>
    
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
        /// <summary>
        /// metoda walidująca datetime
        /// </summary>
        /// <param name="orderDate">data zamówienia</param>
        /// <returns>prawda lub fałsz</returns>

        public bool DateTimeValidation(string orderDate) 
        {
            DateTime data1 = DateTime.Now;
            bool isValid =  DateTime.TryParse(orderDate, out data1);
            return isValid;
        }
        /// <summary>
        /// metoda konwertująca string do datetime
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        
        public DateTime ConvertStringDateTime(string orderDate)
        {
            DateTime data1 = DateTime.Now;
            DateTime.TryParse(orderDate, out data1);
            return data1;
        }
        /// <summary>
        /// metoda walidująca ilość (nie mogą być wpisane inne znaki niż 0-9
        /// </summary>
        /// <param name="text">wartość stringa</param>
        /// <returns>prawda/fałsz</returns>
     
        public bool QuantityValidation(string text)
        {
            Regex _regex = new Regex("[^0-9.-]+");
            return !_regex.IsMatch(text);
        }
    }
}
