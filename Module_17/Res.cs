using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_17
{
    internal class Res
    {
        public static List<TableTemplate> resultModels = new List<TableTemplate>();
        
        public static List<TableTemplate> Method()
        {

            resultModels.Clear();
            using (MySqlConnection mySqlConnection = new MySqlConnection(MySqlDataServer.stringBuilder.ConnectionString))
                using( SqlConnection sqlConnection = new SqlConnection(MainWindow.strSqlConClient.ConnectionString))
            {
                    mySqlConnection.Open();
                sqlConnection.Open();


                var sql = @"SELECT 
                    Clients.id as 'Id',
                    Clients.Email as 'Email',
                    Clients.FirstName as 'FirstName'
                    From Clients";

                SqlCommand sqlcmd = sqlConnection.CreateCommand();
                sqlcmd.CommandText = sql;
                sqlcmd.CommandType = System.Data.CommandType.Text;
                var mysql = @"SELECT
                        orders.Email as 'Email2',
                        orders.ProductId as 'ProductId',
                        orders.ProductName as 'ProductName'
                        FROM orders";
                MySqlCommand mysqlcmd = mySqlConnection.CreateCommand();
                mysqlcmd.CommandText = mysql;
                mysqlcmd.CommandType = System.Data.CommandType.Text;

                var DS = new DataSet();
                var DA = new SqlDataAdapter();
                DA.SelectCommand = sqlcmd;
                DA.Fill(DS);

                var myDS = new DataSet();
                var myDA = new MySqlDataAdapter();
                myDA.SelectCommand = mysqlcmd;
                myDA.Fill(myDS);

                var clients = DS.Tables[0].AsEnumerable();
                var orders = myDS.Tables[0].AsEnumerable();
                var result = (from clientsDr in clients
                              from ordersDr in orders

                              select new TableTemplate
                              {
                                  Id = (int)clientsDr["id"],
                                  FirstName = (string)clientsDr["FirstName"],
                                  Email = (string)clientsDr["Email"],
                                  Email2 = (string)ordersDr["Email2"],
                                  ProductId = (int)ordersDr["ProductId"],
                                  ProductName = (string)ordersDr["ProductName"]
                              
                              }); ;
                foreach (TableTemplate model in result)
                {

                    if (model.Email == model.Email2)
                    {
                        resultModels.Add(model);
                    }                    
                }

                sqlConnection.Close();
                sqlConnection.Dispose();
                mySqlConnection.Close();
                mySqlConnection.Dispose();
                return resultModels;
            }
           
        }
    }
}
