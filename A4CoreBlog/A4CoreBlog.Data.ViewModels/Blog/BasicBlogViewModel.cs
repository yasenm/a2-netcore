using System;

namespace A4CoreBlog.Data.ViewModels
{
    public class BasicBlogViewModel : DescribableViewModel
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public DateTime From { get; set; }
    }
}