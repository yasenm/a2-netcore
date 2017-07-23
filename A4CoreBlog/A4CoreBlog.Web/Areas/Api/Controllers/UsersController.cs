using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            return _userService.GetAll<UserBaseViewModel>();
        }

        [HttpGet]
        public UserProfileViewModel Details(string username)
        {
            return _userService.Get<UserProfileViewModel>(username);
        }
    }
}
