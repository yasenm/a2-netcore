using A4CoreBlog.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace A4CoreBlog.Data
{
    public interface IBlogSystemContext
    {
        DbSet<Blog> Blogs { get; set; }
        DbSet<BlogComment> BlogComments { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<PostComment> PostComments { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<SystemImage> Images { get; set; }
    }
}
