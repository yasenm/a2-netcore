using A4CoreBlog.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace A4CoreBlog.Data
{
    public interface IBlogSystemContext
    {
        DbSet<Blog> Blogs { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<SystemImage> Images { get; set; }
    }
}
