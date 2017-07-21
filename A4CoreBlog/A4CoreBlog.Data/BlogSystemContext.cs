using A4CoreBlog.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace A4CoreBlog.Data
{
    public class BlogSystemContext : IdentityDbContext<User>, IBlogSystemContext
    {
        private IConfigurationRoot _config;

        public BlogSystemContext(IConfigurationRoot config, DbContextOptions options)
            : base(options)
        {
            _config = config;
        }

        public BlogSystemContext()
            : base()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<SystemImage> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Entity<User>()
                .HasOne(u => u.Blog)
                .WithOne(b => b.Owner)
                .HasForeignKey<Blog>(b => b.OwnerId);
            //modelBuilder.Entity<Blog>()
            //    .HasOne(u => u.Owner)
            //    .WithOne(u => u.Blog)
            //    .HasForeignKey<User>(b => b.BlogId);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Posts)
            //    .WithOne(p => p.Author)
            //    .HasForeignKey(p => p.AuthorId);

            //modelBuilder.Entity<Blog>()
            //    .HasMany(b => b.Posts)
            //    .WithOne(p => p.Blog)
            //    .HasForeignKey(p => p.BlogId);
        }
    }
}
