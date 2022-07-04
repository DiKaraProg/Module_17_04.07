using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Sample.xaml
    /// </summary>
    public partial class AllOrdersTable : Window
    {
        public AllOrdersTable()
        {
            InitializeComponent();
            View();

        }
        public void View()
        {
            DataGrid.DataContext = Res.Method();
        }
    }
}
