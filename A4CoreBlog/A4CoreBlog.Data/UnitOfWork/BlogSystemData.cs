using A4CoreBlog.Data.Common.Models;
using A4CoreBlog.Data.Common.Repositories;
using A4CoreBlog.Data.Models;
using A4CoreBlog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.UnitOfWork
{
    public class BlogSystemData
    {
        private IDictionary<Type, object> repositories;

        public BlogSystemData(BlogSystemContext context)
        {
            Context = context;
            repositories = new Dictionary<Type, object>();
        }

        public BlogSystemContext Context { get; set; }

        public IRepository<User> Users
        {
            get
            {
                return GetRepository<User>();
            }
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.Context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            var typeOfRepository = typeof(T);
            if (!repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(DeletableEntityRepository<T>), this.Context);
                repositories.Add(typeOfRepository, newRepository);
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeOfRepository];
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }
    }
}
