using A4CoreBlog.Data.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace A4CoreBlog.Data.Models
{
    public class Post : DescribableEntity
    {
        private ICollection<PostComment> _comments;

        public Post()
        {
            _comments = new HashSet<PostComment>();
        }

        [Key]
        public int Id { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public virtual ICollection<PostComment> Comments
        {
            get => _comments;
            set => _comments = value;
        }
    }
}