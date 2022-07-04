using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Module_17
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddOrders : Window
    {
        

        public AddOrders()
        {
            InitializeComponent();

        }
       
        private void AddButton1_Click(object sender, RoutedEventArgs e)
        {
            MySqlDataServer mySql = new MySqlDataServer();
            mySql.MyInsert(Email_add.Text, Convert.ToInt32(Product_Id_add.Text), Product_Name_add.Text);
            this.Close();
           
        }

       
    }
}
