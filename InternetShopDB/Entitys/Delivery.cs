using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopDB
{
    public class Delivery
    {
        [Key]
        public int OrderId { get; set; }
        public string? Address { get; set; }
        public virtual Order? Order { get; set; }

    }
}
