using System;

namespace A4CoreBlog.Data.ViewModels
{
    public class BaseCommentViewModel
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
