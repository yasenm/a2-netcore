namespace A4CoreBlog.Data.ViewModels
{
    public class PostEditViewModel : DescribableViewModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public int BlogId { get; set; }
    }
}
