using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopDB
{
    public class Stock
    {
        public int StorageId { get; set; }
        public virtual Storage? Storage { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Count { get; set; }
    }
}
