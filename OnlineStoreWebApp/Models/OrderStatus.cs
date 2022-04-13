using System;
using System.Collections.Generic;

namespace OnlineStoreWebApp
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
