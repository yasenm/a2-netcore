using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;

namespace A4CoreBlog.Web.Areas.Api.Controllers
{
    public class UsersController : BaseApiAreaController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<UserBaseViewModel> All()
        {
            return _userService.GetAll<UserBaseViewModel>(GlobalConstants.TeamMemberRole);
        }

        [HttpGet]
        public UserProfileViewModel Details(string username)
        {
            return _userService.Get<UserProfileViewModel>(username);
        }

        [HttpGet]
        [Authorize]
        public UserProfileViewModel Profile()
        {
            var username = User.GetJwtTokenUserName();
            return _userService.Get<UserProfileViewModel>(username);
        }
    }
}
