using A4CoreBlog.Common;
using A4CoreBlog.Data.Infrastructure;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Api.Controllers
{
    [Area(GlobalConstants.APIArea)]
    public class ClientAuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IOptions<AppConfiguration> _appConfiguration;

        public ClientAuthController(IAuthService authService,
            IOptions<AppConfiguration> appConfiguration)
        {
            _authService = authService;
            _appConfiguration = appConfiguration;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterUser(model, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return new BadRequestObjectResult(ModelState);
            }

            return new OkResult();
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.CheckUser(model);
            if (result)
            {
                var token = await _authService.GetJwtSecurityToken(model);
                if (token != null)
                {
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<int> RandomN()
        {
            return 42;
        }
    }
}
