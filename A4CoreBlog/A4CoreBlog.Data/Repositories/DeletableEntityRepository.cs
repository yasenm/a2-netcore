using A4CoreBlog.Data.Common.Models;
using A4CoreBlog.Data.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace A4CoreBlog.Data.Repositories
{
    public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T> where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public override void Delete(T entity)
        {
            entity.DeletedOn = DateTime.Now;
            entity.IsDeleted = true;

            var entry = this.DbContext.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
