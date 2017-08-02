using A4CoreBlog.Common;
using A4CoreBlog.Data.Infrastructure;
using A4CoreBlog.Data.Models;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.UnitOfWork;
using A4CoreBlog.Data.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IBlogSystemContext _ctx;
        private readonly IBlogSystemData _data;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IOptions<AppConfiguration> _appConfiguration;

        public AuthService(IBlogSystemContext ctx,
            IBlogSystemData data,
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher,
            IOptions<AppConfiguration> appConfiguration)
        {
            _ctx = ctx;
            _data = data;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _appConfiguration = appConfiguration;
        }

        public async Task<bool> CheckUser<T>(T model)
        {
            var userModel = Mapper.Map<LoginViewModel>(model);
            var user = await _userManager.FindByNameAsync(userModel.Username);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userModel.Password) != PasswordVerificationResult.Success)
            {
                return false;
            }
            return true;
        }

        public async Task<IdentityResult> RegisterUser<T>(T userModel, string password)
        {
            var userIdentity = Mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(userIdentity, password);
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(userIdentity, new Claim("role", GlobalConstants.RegularUserRole));
            }
            return result;
        }

        /// <summary>
        /// Generate JWT Token based on valid User
        /// </summary>
        public async Task<JwtSecurityToken> GetJwtSecurityToken<T>(T model)
        {
            try
            {
                var userModel = Mapper.Map<User>(model);
                var user = await _userManager.FindByNameAsync(userModel.UserName);
                var userClaims = await _userManager.GetClaimsAsync(user);

                return new JwtSecurityToken(
                    issuer: _appConfiguration.Value.SiteUrl,
                    audience: _appConfiguration.Value.SiteUrl,
                    claims: GetTokenClaims(user).Union(userClaims),
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfiguration.Value.Key)),
                        SecurityAlgorithms.HmacSha256)
                );
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Generate additional JWT Token Claims.
        /// Use to any additional claims you might need.
        /// https://self-issued.info/docs/draft-ietf-oauth-json-web-token.html#rfc.section.4
        /// </summary>
        private static IEnumerable<Claim> GetTokenClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
            };
        }
    }
}
