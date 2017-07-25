using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace A4CoreBlog.Data.Services.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser<T>(T userModel, string password);
    }
}
