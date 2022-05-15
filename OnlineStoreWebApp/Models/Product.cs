using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreWebApp
{
    public partial class Product
    {
        public Product()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage="Field can`t be empty")]
        [Display(Name="Product")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Field can`t be empty")]
        public decimal Price { get; set; }
        public string Info { get; set; }
        public string Img { get; set; }
        public bool isInStock { get; set; } = false;
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
