using System.Linq;

namespace A4CoreBlog.Data.Common.Repositories
{
    public interface IDeletableEntityRepository<T> : IRepository<T> where T : class
    {
        IQueryable<T> AllWithDeleted();
    }
}
