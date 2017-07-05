using System.Collections.Generic;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface IPostService
    {
        ICollection<T> GetAll<T>();
        T Get<T>(int id);
    }
}
