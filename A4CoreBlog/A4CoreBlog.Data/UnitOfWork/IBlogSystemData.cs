using A4CoreBlog.Data.Common.Repositories;
using A4CoreBlog.Data.Models;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.UnitOfWork
{
    public interface IBlogSystemData
    {
        IRepository<User> Users { get; }
        IRepository<Blog> Blogs { get; }
        IRepository<Post> Posts { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
