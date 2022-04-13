using System;
using System.Collections.Generic;

namespace OnlineStoreWebApp
{
    public partial class City
    {
        public City()
        {
            PostOffices = new HashSet<PostOffice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<PostOffice> PostOffices { get; set; }
    }
}
