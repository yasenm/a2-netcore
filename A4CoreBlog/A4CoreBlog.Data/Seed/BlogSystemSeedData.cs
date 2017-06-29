using A4CoreBlog.Common.RandomGenerators;
using A4CoreBlog.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
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
            //_context.Database.EnsureDeleted();
            await SeedUsers();
            if (_context.Database.EnsureCreated())
            {
            }
        }

        private async Task SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                if (await _userManager.FindByEmailAsync("admin@core.com") == null)
                {
                    var user = new User
                    {
                        UserName = "admin@core.com",
                        Email = "admin@core.com",
                    };

                    await _userManager.CreateAsync(user, "P@ssw0rd");
                }

                for (int i = 0; i < NumberGenerator.RandomNumber(6, 12); i++)
                {
                    var user = new User
                    {
                        UserName = StringGenerator.RandomStringWithSpaces(4, 20),
                        Email = StringGenerator.RandomStringWithoutSpaces(2, 7) + "@core.com"
                    };

                    await _userManager.CreateAsync(user, "P@ssw0rd");
                }
            }
        }
    }
}
