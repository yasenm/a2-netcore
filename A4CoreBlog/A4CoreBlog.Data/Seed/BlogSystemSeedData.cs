using A4CoreBlog.Common;
using A4CoreBlog.Common.RandomGenerators;
using A4CoreBlog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.Seed
{
    public class BlogSystemSeedData
    {
        private BlogSystemContext _context;
        private UserManager<User> _userManager;

        public BlogSystemSeedData(BlogSystemContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedData()
        {
            await _context.Database.EnsureDeletedAsync();
            if (await _context.Database.EnsureCreatedAsync())
            {
                await SeedRoles();
                await SeedUsers();
                await SeedBlogs();
                await SeedPosts();
            }
        }

        private async Task SeedRoles()
        {
            if (!_context.Roles.Any())
            {
                await _context.Roles.AddAsync(new IdentityRole(GlobalConstants.AdminRole));
                await _context.Roles.AddAsync(new IdentityRole(GlobalConstants.RegularUser));
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                await SeedAdmin();
                await SeedOtherUsers();
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedAdmin()
        {
            var userEmail = "admin@core.com";
            var adminUser = new User
            {
                UserName = userEmail,
                Email = userEmail
            };

            var roles = _context.Roles.ToList();
            var result = await _userManager.CreateAsync(adminUser, "P@ssw0rd");
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(adminUser, new Claim("role", GlobalConstants.AdminRole));
            }
            await _context.SaveChangesAsync();
        }

        private async Task SeedOtherUsers()
        {
            for (int i = 0; i < 3; i++)
            {
                var userEmail = StringGenerator.RandomStringWithoutSpaces(7, 10) + "@core.com";
                var user = new User
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FirstName = StringGenerator.RandomStringWithoutSpaces(3, 10),
                    LastName = StringGenerator.RandomStringWithoutSpaces(7, 20),
                    Profession = StringGenerator.RandomStringWithoutSpaces(3, 20),
                    AvatarLink = "https://cdn.pixabay.com/photo/2016/03/28/12/35/cat-1285634_960_720.png"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("role", GlobalConstants.RegularUser));
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task SeedBlogs()
        {
            if (!_context.Blogs.Any())
            {
                var users = _context.Users.ToList();
                for (int i = 0; i < users.Count - 1; i++)
                {
                    Blog newBlog = new Blog()
                    {
                        Description = StringGenerator.RandomStringWithSpaces(200, 400),
                        Title = StringGenerator.RandomStringWithSpaces(6, 50),
                        Summary = StringGenerator.RandomStringWithSpaces(80, 100),
                        OwnerId = users[i].Id
                    };

                    await _context.Blogs.AddAsync(newBlog);
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedPosts()
        {
            if (!_context.Posts.Any())
            {
                var users = _context.Users.ToList();
                var blogs = _context.Blogs.ToList();
                for (int i = 0; i < NumberGenerator.RandomNumber(6, 7); i++)
                {
                    Post newPost = new Post()
                    {
                        Description = StringGenerator.RandomStringWithSpaces(200, 400),
                        Title = StringGenerator.RandomStringWithSpaces(6, 50),
                        Summary = StringGenerator.RandomStringWithSpaces(80, 100),
                        AuthorId = users[NumberGenerator.RandomNumber(0, users.Count - 1)].Id,
                        BlogId = blogs[NumberGenerator.RandomNumber(0, blogs.Count - 1)].Id
                    };

                    await _context.Posts.AddAsync(newPost);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
