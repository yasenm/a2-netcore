using System.Collections.Generic;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface IUserService
    {
        T Get<T>(string username);
        ICollection<T> GetAll<T>();
        T Update<T>(T model);
    }
}