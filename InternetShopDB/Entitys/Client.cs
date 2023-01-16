using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InternetShopDB
{
    [Table("Client")]
    public class Client:Person
    {
        [MaxLength(10)]
        public string PhoneNumb { get; set; }
        public string Email { get; set; }
        public virtual List<Order> Orders { get; set; } = new();
    }
}
