using System.Linq;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface IBlogService
    {
        T Get<T>(int id);
        IQueryable<T> GetAll<T>();
    }
}
