using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminArea)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [Authorize]
        public IActionResult MyProfile()
        {
            var currentUsername = User.Identity.Name;
            var model = _userService.Get<UserProfileViewModel>(currentUsername);
            return View(model);
        }
    }
}
