using System;

namespace A4CoreBlog.Data.ViewModels
{
    public class PostListBasicViewModel : IVIewModel
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int BlogId { get; set; }
        public DateTime From { get; set; }
    }
}
