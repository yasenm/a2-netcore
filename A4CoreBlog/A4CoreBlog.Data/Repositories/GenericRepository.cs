using A4CoreBlog.Data.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace A4CoreBlog.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public GenericRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }
        public virtual IQueryable<T> All()
        {
            return DbSet;
        }

        public virtual T GetById(object id)
        {
            //EF Core Will be updated shortly with Find Extension by Default
            //Here, I have written Extension method for the same
            //Source:- http://stackoverflow.com/questions/29030472/dbset-doesnt-have-a-find-method-in-ef7/29082410#29082410
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            EntityEntry<T> dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != (EntityState)EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            EntityEntry<T> dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != (EntityState)EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            EntityEntry<T> dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != (EntityState)EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);
            if (entity == null) return;

            Delete(entity);
        }

        public virtual void Detach(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateValues(Expression<Func<T, object>> entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
