using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Total amount")]
        public decimal TotalAmount { get; set; }
        public int OrderStatusId { get; set; }
        [Display(Name = "Date ordered")]
        public DateTime DateOrdered { get; set; }
        [Display(Name = "Date arrived")]
        public DateTime? DateArrived { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        [Display(Name = "Order status")]
        public virtual OrderStatus OrderStatus { get; set; } = null!;
        public virtual Saller Saller { get; set; } = null!;
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
