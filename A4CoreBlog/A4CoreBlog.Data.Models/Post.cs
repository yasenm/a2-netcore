using A4CoreBlog.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace A4CoreBlog.Data.Models
{
    public class Post : DescribableEntity
    {
        [Key]
        public int Id { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}