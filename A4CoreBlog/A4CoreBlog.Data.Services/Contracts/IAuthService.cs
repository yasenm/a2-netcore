using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser<T>(T userModel, string password);
        Task<bool> CheckUser<T>(T model);
        Task<JwtSecurityToken> GetJwtSecurityToken<T>(T model);
    }
}
