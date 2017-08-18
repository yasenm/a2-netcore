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
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SystemImage> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Blog)
                .WithOne(b => b.Owner)
                .HasForeignKey<Blog>(b => b.OwnerId);
        }
    }
}
