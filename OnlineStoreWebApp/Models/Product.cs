using System;
using System.Collections.Generic;

namespace OnlineStoreWebApp
{
    public partial class Product
    {
        public Product()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
