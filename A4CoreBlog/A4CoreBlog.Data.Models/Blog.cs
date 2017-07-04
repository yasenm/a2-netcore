using A4CoreBlog.Data.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace A4CoreBlog.Data.Models
{
    public class Blog : DescribableEntity
    {
        private ICollection<Post> _posts;

        public Blog()
        {
            _posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Post> Posts
        {
            get => _posts;
            set => _posts = value;
        }
    }
}
