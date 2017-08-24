using System.ComponentModel.DataAnnotations;

namespace A4CoreBlog.Data.ViewModels
{
    public class PostACommentViewModel
    {
        [MaxLength(1000)]
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int ForId { get; set; }
    }
}
