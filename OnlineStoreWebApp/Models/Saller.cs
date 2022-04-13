﻿using System;
using System.Collections.Generic;

namespace OnlineStoreWebApp
{
    public partial class Saller
    {
        public Saller()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public decimal PhoneNumber { get; set; }
        public int PostOfficeId { get; set; }

        public virtual PostOffice PostOffice { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
