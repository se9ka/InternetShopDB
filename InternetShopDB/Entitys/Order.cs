using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopDB
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
        public int EmployeeId{ get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Delivery? Delivery { get; set; }
        public Decimal Sum { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string KindOfDelivery { get; set; }
        public string KindOfPayment { get; set; }
        public string StatusOrder{ get; set; }
        public virtual List<ProductInOrder> ProductsInOrders { get; set; } = new();

    }
}
