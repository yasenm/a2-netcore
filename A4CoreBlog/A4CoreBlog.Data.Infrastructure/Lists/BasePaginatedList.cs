namespace A4CoreBlog.Data.Infrastructure
{
    public class BasePaginatedList
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
