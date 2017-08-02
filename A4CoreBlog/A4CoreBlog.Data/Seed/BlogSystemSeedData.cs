using A4CoreBlog.Common;
using A4CoreBlog.Common.RandomGenerators;
using A4CoreBlog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.Seed
{
    public class BlogSystemSeedData
    {
        private readonly BlogSystemContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BlogSystemSeedData(BlogSystemContext context, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {
            //await _context.Database.EnsureDeletedAsync();
            //if (await _context.Database.EnsureCreatedAsync())
            //{
                await SeedRoles();
                await SeedUsers();
                await SeedBlogs();
                await SeedPosts();
            //}
        }

        private async Task SeedRoles()
        {
            if (!_context.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdminRole));
                await _roleManager.CreateAsync(new IdentityRole(GlobalConstants.TeamMemberRole));
                await _roleManager.CreateAsync(new IdentityRole(GlobalConstants.RegularUserRole));
                //await _context.SaveChangesAsync();
            }
        }

        private async Task SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                await SeedAdmin();
                await SeedTeamMembers();
                await SeedOtherUsers();
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedAdmin()
        {
            var userEmail = "admin@core.com";
            var adminUser = GenerateUser(userEmail);
            var roles = _context.Roles.ToList();
            var result = await _userManager.CreateAsync(adminUser, "P@ssw0rd");
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(adminUser, new string[] { GlobalConstants.AdminRole, GlobalConstants.TeamMemberRole });
                await _userManager.AddClaimsAsync(adminUser, new Claim[] { new Claim("role", GlobalConstants.AdminRole), new Claim("role", GlobalConstants.TeamMemberRole) });
            }
            await _context.SaveChangesAsync();
        }

        private async Task SeedTeamMembers()
        {
            for (int i = 0; i < 2; i++)
            {
                var userEmail = StringGenerator.RandomStringWithoutSpaces(7, 10) + "@core.com";
                var user = GenerateUser(userEmail);

                var result = await _userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, GlobalConstants.TeamMemberRole);
                    await _userManager.AddClaimAsync(user, new Claim("role", GlobalConstants.TeamMemberRole));
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task SeedOtherUsers()
        {
            for (int i = 0; i < 3; i++)
            {
                var userEmail = StringGenerator.RandomStringWithoutSpaces(7, 10) + "@core.com";
                var user = GenerateUser(userEmail);

                var result = await _userManager.CreateAsync(user, "P@ssw0rd");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, GlobalConstants.RegularUserRole);
                    await _userManager.AddClaimAsync(user, new Claim("role", GlobalConstants.RegularUserRole));
                }
            }
            await _context.SaveChangesAsync();
        }

        private User GenerateUser(string email)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                FirstName = StringGenerator.RandomStringWithoutSpaces(3, 10),
                LastName = StringGenerator.RandomStringWithoutSpaces(7, 20),
                Profession = StringGenerator.RandomStringWithoutSpaces(3, 20),
                AvatarLink = "https://cdn.pixabay.com/photo/2016/03/28/12/35/cat-1285634_960_720.png"
            };
            return user;
        }

        private async Task SeedBlogs()
        {
            if (!_context.Blogs.Any())
            {
                var teamMemberRole = _context.Roles.FirstOrDefault(r => r.Name == GlobalConstants.TeamMemberRole);
                if (teamMemberRole != null)
                {
                    var users = _context.Users
                        .Where(u =>
                            u.Claims.FirstOrDefault(c => c.ClaimValue == teamMemberRole.Name) != null)
                        .ToList();
                    for (int i = 0; i < users.Count; i++)
                    {
                        Blog newBlog = new Blog()
                        {
                            Description = StringGenerator.RandomStringWithSpaces(200, 400),
                            Title = StringGenerator.RandomStringWithSpaces(6, 50),
                            Summary = StringGenerator.RandomStringWithSpaces(80, 100),
                            OwnerId = users[i].Id,
                            CreatedOn = DateTime.Now
                        };

                        await _context.Blogs.AddAsync(newBlog);
                    }
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task SeedPosts()
        {
            if (!_context.Posts.Any())
            {
                var blogs = _context.Blogs.ToList();
                foreach (var blog in blogs)
                {
                    for (int i = 0; i < NumberGenerator.RandomNumber(0, 5); i++)
                    {
                        Post newPost = new Post()
                        {
                            Description = StringGenerator.RandomStringWithSpaces(200, 400),
                            Title = StringGenerator.RandomStringWithSpaces(6, 50),
                            Summary = StringGenerator.RandomStringWithSpaces(80, 100),
                            AuthorId = blog.OwnerId,
                            BlogId = blog.Id,
                            CreatedOn = DateTime.Now
                        };

                        await _context.Posts.AddAsync(newPost);
                    }
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
