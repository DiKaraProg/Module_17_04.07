using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_17
{
    public class MySqlDataServer
    {
        public static MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Port = 3306,
            Password = "root",
            UserID = "root",
            Database = "mysqldataserver"
        };
        public MySqlConnection mySqlConnection = new MySqlConnection(stringBuilder.ConnectionString);
        public MySqlDataAdapter adapterOrders;
        public static DataTable tableOrders;

        public void OpenConnection()
        {
            if (mySqlConnection.State == ConnectionState.Closed)
            {
                mySqlConnection.Open();
            }
        }
        public void CloseConnection()
        {
            if (mySqlConnection.State == ConnectionState.Open)
            {
                mySqlConnection.Close();
            }
        }
        public MySqlConnection GetConnection()
        {
            return mySqlConnection;
        }
        public void MyInsert(string Email, int ProductId, string ProductName)
        {
            mySqlConnection = new MySqlConnection(stringBuilder.ConnectionString);
            mySqlConnection.Open();
            try
            {
                string sql = "INSERT INTO orders (Email, ProductId, ProductName)" +
                   "VALUES (@Email,@ProductId,@ProductName)";
                MySqlCommand cmd = mySqlConnection.CreateCommand();
                cmd.CommandText = sql;

                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = ProductId;
                cmd.Parameters.Add("@ProductName", MySqlDbType.VarChar).Value = ProductName;
                int rowCont = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {


            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();

            }


            
        }
        public static  DataTable MySelect()
        {
            DataTable tableOrders = new DataTable();
            MySqlDataAdapter adapterOrders = new MySqlDataAdapter();
            MySqlConnection mySqlConnection = new MySqlConnection(stringBuilder.ConnectionString);
            mySqlConnection.Open();
            try
            {
                var sql = @"SELECT * FROM `orders`";
                var cmd = mySqlConnection.CreateCommand();
                cmd.CommandText = sql;
                adapterOrders.SelectCommand = cmd;
                adapterOrders.Fill(tableOrders);
            }
            catch (Exception)
            {
                
                
            }
            finally
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }

            return tableOrders;


        }

        public static void MyDelete(int Id)
        {
            var mySqlConnection = new MySqlConnection(stringBuilder.ConnectionString);
            mySqlConnection.Open();
            try
            {
               var sql = @"DELETE FROM orders WHERE Id= @Id";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;
                cmd.ExecuteNonQuery(); 
            }
            catch (Exception)
            {

                
            }
            finally 
            { 
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
            
            
           
        }
        public static void MyUpdate(int Id,string Em,int PrId,string Name)
        {
            string Error= null;
            var mySqlConnection = new MySqlConnection(stringBuilder.ConnectionString);
            mySqlConnection.Open();
            try
            {
                var sql = @"UPDATE orders SET 
        Email= @Email,
        ProductId= @ProductId,
        ProductName= @ProductName
        WHERE Id= @Id";
                var cmd = new MySqlCommand(sql, mySqlConnection);
                cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Id;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Em;
                cmd.Parameters.Add("@ProductId", MySqlDbType.Int32).Value = PrId;
                cmd.Parameters.Add("@ProductName", MySqlDbType.VarChar).Value = Name;
                int rowCont = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                Error= e.Message;
            }
            finally 
            {
                mySqlConnection.Close();
                mySqlConnection.Dispose();
            }
            
        }

    }
}
