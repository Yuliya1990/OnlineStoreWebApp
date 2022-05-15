using OnlineStoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreWebApp
{
    public partial class OrderLine
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ShopCartId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product Product { get; set; } = null!;

    }
}
