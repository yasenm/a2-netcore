using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.UnitOfWork;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace A4CoreBlog.Data.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBlogSystemData _data;

        public UserService(IBlogSystemData data)
        {
            _data = data;
        }

        public T Get<T>(string username)
        {
            var found = _data.Users.All().FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
            var result = AutoMapper.Mapper.Map<T>(found);
            return result;
        }

        public ICollection<T> GetAll<T>()
        {
            return _data.Users.All().ProjectTo<T>().ToList();
        }

        public T Update<T>(T model)
        {
            throw new NotImplementedException();
        }
    }
}
