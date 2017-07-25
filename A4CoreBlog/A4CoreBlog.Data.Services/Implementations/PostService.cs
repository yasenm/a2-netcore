using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.UnitOfWork;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using System;
using A4CoreBlog.Data.Models;
using AutoMapper;

namespace A4CoreBlog.Data.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IBlogSystemData _data;

        public PostService(IBlogSystemData data)
        {
            _data = data;
        }

        public bool AddOrUpdate<T>(T model)
        {
            try
            {
                var dbModel = Mapper.Map<Post>(model);
                if (dbModel.Id != 0)
                {
                    _data.Posts.Update(dbModel);
                }
                else
                {
                    var blogId = _data.Blogs.All().FirstOrDefault(b => b.OwnerId == dbModel.AuthorId).Id;
                    dbModel.BlogId = blogId;
                    _data.Posts.Add(dbModel);
                }
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public T Get<T>(int id)
        {
            var model = _data.Posts.All()
                .Where(p => p.Id == id)
                .ProjectTo<T>()
                .FirstOrDefault();

            return model;
        }

        public IQueryable<T> GetAll<T>()
        {
            return _data.Posts.All().ProjectTo<T>();
        }
    }
}
