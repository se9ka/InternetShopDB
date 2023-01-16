using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopDB
{
    public class Storage
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public virtual List<Stock> Stocks { get; set; } = new();
    }
}
