using System.Linq;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface ISystemImageService
    {
        T Get<T>(int id);
        IQueryable<T> GetCollection<T>();
        T AddOrUpdate<T>(T model);
    }
}
