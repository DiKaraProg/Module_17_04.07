using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_17
{
    public class TableTemplate
    {
        int id;
        string email;
        string email2;
        string firstName;
        int productId;
        string productName;

        public int Id { get { return id; } set { id = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Email2 { get { return email2; } set { email2 = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public int ProductId { get { return productId; } set { productId = value; } }
        public string ProductName { get { return productName; } set { productName = value; } }
        public TableTemplate()
        {

        }
    }
    
}
