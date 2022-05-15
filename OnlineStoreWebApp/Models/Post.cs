using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreWebApp
{
    public partial class Post
    {
        public Post()
        {
            PostOffices = new HashSet<PostOffice>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Field cannot be empty!")]
        public string Name { get; set; } = null!;

        public virtual ICollection<PostOffice> PostOffices { get; set; }
    }
}
