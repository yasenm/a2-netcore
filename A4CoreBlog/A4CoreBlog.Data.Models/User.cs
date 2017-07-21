using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace A4CoreBlog.Data.Models
{
    public class User : IdentityUser
    {
        private ICollection<Post> _posts;

        public User()
        {
            _posts = new HashSet<Post>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Profession { get; set; }
        public string AvatarLink { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual ICollection<Post> Posts
        {
            get => _posts;
            set => _posts = value;
        }
    }
}
