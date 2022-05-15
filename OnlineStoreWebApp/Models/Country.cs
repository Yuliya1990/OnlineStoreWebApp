using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreWebApp
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Field cannot be empty!")]
        public string Name { get; set; } = null!;

        public virtual ICollection<City> Cities { get; set; }
    }
}
