using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopDB
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public virtual List<Product> Products { get; set; } = new();

        public static implicit operator List<object>(Provider v)
        {
            throw new NotImplementedException();
        }
    }
}
