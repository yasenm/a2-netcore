using A4CoreBlog.Data.Common.Models;

namespace A4CoreBlog.Data.Models
{
    public class Comment : DeletableEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}
