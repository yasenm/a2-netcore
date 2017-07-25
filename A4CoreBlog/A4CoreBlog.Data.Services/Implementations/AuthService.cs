using System;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using A4CoreBlog.Data.Models;
using AutoMapper;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IBlogSystemContext _ctx;
        private readonly IBlogSystemData _data;
        private readonly UserManager<User> _userManager;

        public AuthService(IBlogSystemContext ctx, 
            IBlogSystemData data,
            UserManager<User> userManager)
        {
            _ctx = ctx;
            _data = data;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser<T>(T userModel, string password)
        {
            var userIdentity = Mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(userIdentity, password);
            
            return result;
        }
    }
}
