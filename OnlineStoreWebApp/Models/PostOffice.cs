using System;
using System.Collections.Generic;

namespace OnlineStoreWebApp
{
    public partial class PostOffice
    {
        public PostOffice()
        {
            Customers = new HashSet<Customer>();
            Sallers = new HashSet<Saller>();
        }

        public int Id { get; set; }
        public int PostId { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Saller> Sallers { get; set; }
    }
}
