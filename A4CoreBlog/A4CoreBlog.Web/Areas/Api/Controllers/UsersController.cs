using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;

namespace A4CoreBlog.Web.Areas.Api.Controllers
{
    [Area(GlobalConstants.APIArea)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<UserBaseViewModel> All()
        {
            Thread.Sleep(2000);
            return _userService.GetAll<UserBaseViewModel>(GlobalConstants.TeamMemberRole);
        }

        [HttpGet]
        public UserProfileViewModel Details(string username)
        {
            Thread.Sleep(2000);
            return _userService.Get<UserProfileViewModel>(username);
        }
    }
}
