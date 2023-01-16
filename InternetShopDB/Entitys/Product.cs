using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopDB
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal CurPrice { get; set; }
        public string Category { get; set; }
        public string Producer { get; set; }
        public string? Description { get; set; }
        public virtual List<ProductInOrder> ProductsInOrders { get; set; } = new();
        public virtual List<Provider> Providers { get; set; } = new();
        public virtual List<Stock> Stocks { get; set; } = new();
    }
}
