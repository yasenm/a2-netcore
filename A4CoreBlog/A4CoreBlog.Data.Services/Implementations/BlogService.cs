using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using A4CoreBlog.Data.Models;

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

        public async Task<bool> Edit<T>(T model)
        {
            try
            {
                Blog blogToEdit = AutoMapper.Mapper.Map<Blog>(model);
                _data.Blogs.Update(blogToEdit);
                await _data.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T Get<T>(int id)
        {
            var result = _data.Blogs.All()
                 .Where(b => b.Id == id)
                 .ProjectTo<T>()
                 .FirstOrDefault();

            return result;
        }

        public T Get<T>(string userId)
        {
            var result = _data.Blogs.All()
                 .Where(b => b.OwnerId == userId)
                 .ProjectTo<T>()
                 .FirstOrDefault();

            return result;
        }

        public IQueryable<T> GetAll<T>()
        {
            return _data.Blogs.All().ProjectTo<T>();
        }
    }
}
