using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreWebApp
{
    public partial class PostOffice
    {
        public PostOffice()
        {
            Customers = new HashSet<Customer>();
            Sallers = new HashSet<Saller>();
        }

        [Required(ErrorMessage = "Field cannot be empty!")]
        [Display(Name = "№")]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        [Display(Name = "Post name")]
        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Saller> Sallers { get; set; }
    }
}
