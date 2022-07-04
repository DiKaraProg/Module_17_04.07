using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.Common;
using MySqlConnector;
using System.Threading;

namespace Module_17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SqlConnectionStringBuilder strSqlConClient;
        public SqlConnection sqlConnectionClient;
        
       
        public DataTable tableClients;

        public DataTable tableOrders = new DataTable();
        public SqlDataAdapter adapterClients;
        public  MySqlDataAdapter adapterOrders;

        public DataRowView rowClients;
        public DataRowView rowOrders;

        public void PreperingSql_Clients()
        {
            strSqlConClient = new SqlConnectionStringBuilder()
            {
                DataSource = @"DESKTOP-4P5JFR4\SQLEXPRESS",
                InitialCatalog = @"MySqlServer",
                IntegratedSecurity = true,
                UserID = "Admin",
                Password = "qwerty"
            };
            sqlConnectionClient = new SqlConnection() { ConnectionString = strSqlConClient.ConnectionString };
            tableClients = new DataTable();
            adapterClients =  new SqlDataAdapter();
            //select
            var sql = @"SELECT * FROM Clients Order by Clients.id";
            adapterClients.SelectCommand = new SqlCommand(sql, sqlConnectionClient);

            
           
            //INSERT
            sql = @"INSERT INTO Clients (MiddleName, FirstName, FatherName, PhoneNumber, Email)
                            VALUES(@MiddleName, @FirstName, @FatherName, @PhoneNumber, @Email)
                            SET @id= @@IDENTITY";
            adapterClients.InsertCommand = new SqlCommand(sql,sqlConnectionClient);
            adapterClients.InsertCommand.Parameters.Clear();
            adapterClients.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id").Direction = ParameterDirection.Output;
            adapterClients.InsertCommand.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 20, "MiddleName");
            adapterClients.InsertCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20, "FirstName");
            adapterClients.InsertCommand.Parameters.Add("@FatherName", SqlDbType.NVarChar, 20, "FatherName");
            adapterClients.InsertCommand.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20, "PhoneNumber");
            adapterClients.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 20, "Email");
            //UPDATE
            sql = @"UPDATE Clients SET
                        MiddleName= @MiddleName,
                        FirstName= @FirstName,
                        FatherName = @FatherName,
                        PhoneNumber= @PhoneNumber,
                        Email= @Email
                    WHERE id = @id";

            adapterClients.UpdateCommand = new SqlCommand(sql, sqlConnectionClient);
            adapterClients.UpdateCommand.Parameters.Clear();
            adapterClients.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id").SourceVersion= DataRowVersion.Original;
            adapterClients.UpdateCommand.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 20, "MiddleName");
            adapterClients.UpdateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20, "FirstName");
            adapterClients.UpdateCommand.Parameters.Add("@FatherName", SqlDbType.NVarChar, 20, "FatherName");
            adapterClients.UpdateCommand.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 20, "PhoneNumber");
            adapterClients.UpdateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 20, "Email");


            sql = @"Delete from Clients where id = @id";
            adapterClients.DeleteCommand = new SqlCommand(sql, sqlConnectionClient);
            adapterClients.DeleteCommand.Parameters.Clear();
            adapterClients.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id");



            adapterClients.Fill(tableClients);
            DataGrid.DataContext = tableClients.DefaultView;
        }
       
        public MainWindow()
        {
            InitializeComponent(); PreperingSql_Clients();
            DataGrid_Orders.DataContext = MySqlDataServer.MySelect();
        }

        private void MenuItem_Click_Add(object sender, RoutedEventArgs e)
        {
            DataRow r = tableClients.NewRow();
            AddClients addClients = new AddClients(r);
            addClients.ShowDialog();
            if (addClients.DialogResult.Value)
            {
                
                tableClients.Rows.Add(r);
                adapterClients.Update(tableClients);
            }
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            rowClients = (DataRowView)DataGrid.SelectedItem;
            rowClients.Row.Delete();
            adapterClients.Update(tableClients);

        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            rowClients = (DataRowView)DataGrid.SelectedItem;
            rowClients.BeginEdit();
            adapterClients.Update(tableClients);
            
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (rowClients == null) return;
            rowClients.Row.EndEdit();
            
            adapterClients.Update(tableClients);
        }

        private void MenuItem_Click_Delete_Orders(object sender, RoutedEventArgs e)
        {
            rowOrders = (DataRowView)DataGrid_Orders.SelectedItem;
            int id =Convert.ToInt32( rowOrders.Row.ItemArray[0].ToString());
            rowOrders.Row.Delete();

            MySqlDataServer.MyDelete(id);
            DataGrid_Orders.DataContext = MySqlDataServer.MySelect();


        }

        private void MenuItem_Click_Add_Orders(object sender, RoutedEventArgs e)
        
        {
            //DataRow r = tableOrders.NewRow();
            AddOrders addProduct = new AddOrders();
           
            addProduct.ShowDialog();

            DataGrid_Orders.DataContext = MySqlDataServer.MySelect();


        }

        private void OrdersWindow_Click(object sender, RoutedEventArgs e)
        {
            AllOrdersTable allOrdersWindow = new AllOrdersTable();
            allOrdersWindow.Show();

        }




        private void DataGrid_Orders_CurrentCellChanged(object sender, EventArgs e)
        {

            if (rowOrders == null) return;
            rowOrders.Row.EndEdit();

            int Id = Convert.ToInt32(rowOrders.Row.ItemArray[0].ToString());
            string Email= rowOrders.Row.ItemArray[1].ToString();
            int ProductId = Convert.ToInt32(rowOrders.Row.ItemArray[2].ToString());
            string ProductName = rowOrders.Row.ItemArray[3].ToString();

            MySqlDataServer.MyUpdate(Id, Email, ProductId, ProductName);
            DataGrid_Orders.DataContext = MySqlDataServer.MySelect();

        }

        private void DataGrid_Orders_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            rowOrders = (DataRowView)DataGrid_Orders.SelectedItem;
            rowOrders.BeginEdit();
            


        }
    }
}
