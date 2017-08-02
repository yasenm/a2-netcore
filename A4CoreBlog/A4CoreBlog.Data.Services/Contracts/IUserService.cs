using System.Linq;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface IUserService
    {
        T Get<T>(string username);
        IQueryable<T> GetAll<T>(string role);
        T Update<T>(T model);
    }
}