using System.Collections.Generic;
using System.Linq;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface IPostService
    {
        IQueryable<T> GetAll<T>();
        T Get<T>(int id);
        bool AddOrUpdate<T>(T model);
    }
}
