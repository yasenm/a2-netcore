using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace A4CoreBlog.Data.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private IBlogSystemContext _context;
        private IBlogSystemData _data;

        public BlogService(IBlogSystemContext context, IBlogSystemData data)
        {
            _context = context;
            _data = data;
        }

        public T Get<T>(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll<T>()
        {
            return _data.Blogs.All().ProjectTo<T>();
        }
    }
}
