using A4CoreBlog.Data.Common.Models;

namespace A4CoreBlog.Data.Models
{
    public class PostComment : DeletableEntity
    {
        public int Id { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
