using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopDB
{
    [Table("Employee")]
    public class Employee:Person
    {
        
        public string PhoneNumb { get; set; }
        public string Email { get; set; }
        public Decimal Salary { get; set; }
        public string TaxpayerId { get; set; }
        public string? Address { get; set; }
        public string Position { get; set; }
        public virtual List<Order> Orders { get; set; } = new();
    }
}
