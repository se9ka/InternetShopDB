using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopDB
{
    public class ProductInOrder
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int OrderID { get; set; }
        public virtual Order? Order { get; set; }
        public int Count { get; set; }
        public Decimal Price { get; set; }

    }
}
