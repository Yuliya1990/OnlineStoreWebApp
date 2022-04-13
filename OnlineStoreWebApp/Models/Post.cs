using System;
using System.Collections.Generic;

namespace OnlineStoreWebApp
{
    public partial class Post
    {
        public Post()
        {
            PostOffices = new HashSet<PostOffice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PostOffice> PostOffices { get; set; }
    }
}
