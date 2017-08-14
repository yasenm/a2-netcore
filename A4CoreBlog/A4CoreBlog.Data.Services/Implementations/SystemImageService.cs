using System;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.UnitOfWork;
using AutoMapper;
using A4CoreBlog.Data.Models;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace A4CoreBlog.Data.Services.Implementations
{
    public class SystemImageService : ISystemImageService
    {
        private readonly IBlogSystemData _data;

        public SystemImageService(IBlogSystemData data)
        {
            _data = data;
        }

        public T AddOrUpdate<T>(T model)
        {
            try
            {
                var dbModel = Mapper.Map<SystemImage>(model);
                _data.Images.Add(dbModel);
                _data.SaveChanges();
                model = Mapper.Map<T>(dbModel);
                return model;
            }
            catch (Exception ex)
            {
                // TODO: return default T or null
                return model;
            }
        }

        public T Get<T>(int id)
        {
            var dbModel = _data.Images.GetById(id);
            var resultModel = Mapper.Map<T>(dbModel);
            return resultModel;
        }

        public IQueryable<T> GetCollection<T>()
        {
            var result = _data.Images.All()
                .ProjectTo<T>();
            return result;
        }
    }
}
