using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.UnitOfWork;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;

namespace A4CoreBlog.Data.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IBlogSystemData _data;

        public PostService(IBlogSystemData data)
        {
            _data = data;
        }

        public T Get<T>(int id)
        {
            var model = _data.Posts.All()
                .Where(p => p.Id == id)
                .ProjectTo<T>()
                .FirstOrDefault();

            return model;
        }

        public ICollection<T> GetAll<T>()
        {
            return _data.Posts.All().ProjectTo<T>().ToList();
        }
    }
}
