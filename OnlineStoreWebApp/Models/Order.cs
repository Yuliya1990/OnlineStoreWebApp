using System;
using System.Collections.Generic;

namespace OnlineStoreWebApp
{
    public partial class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public int SallerId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime? DateArrived { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual OrderStatus OrderStatus { get; set; } = null!;
        public virtual Saller Saller { get; set; } = null!;
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
