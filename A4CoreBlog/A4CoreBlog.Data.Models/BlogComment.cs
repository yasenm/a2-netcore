using A4CoreBlog.Data.Common.Models;

namespace A4CoreBlog.Data.Models
{
    public class BlogComment : DeletableEntity
    {
        public int Id { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
