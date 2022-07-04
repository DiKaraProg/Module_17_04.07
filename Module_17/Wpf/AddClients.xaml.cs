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
    /// Логика взаимодействия для AddClients.xaml
    /// </summary>
    public partial class AddClients : Window
    {

        public AddClients()
        {
            InitializeComponent();
        }
        public AddClients(DataRow row) : this()
        {
            CloseButton.Click += delegate { this.DialogResult = false; };
            AddButton.Click += delegate
            {
                row["MiddleName"] = MiddleName_add.Text;
                row["FirstName"] = FirstName_add.Text;
                row["FatherName"] = FatherName_add.Text;
                row["PhoneNumber"] = PhoneNumber_add.Text;
                row["Email"] = Email_add.Text;
                this.DialogResult = true;
            };

        }

    }       
}
