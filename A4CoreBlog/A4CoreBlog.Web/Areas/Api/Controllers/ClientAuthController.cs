using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Api.Controllers
{
    [Area(GlobalConstants.APIArea)]
    public class ClientAuthController : Controller
    {
        private readonly IAuthService _authService;

        public ClientAuthController(IAuthService authService)
        {
            _authService = authService;
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

            //await _appDbContext.JobSeekers.AddAsync(new JobSeeker { IdentityId = userIdentity.Id, Location = model.Location });
            //await _appDbContext.SaveChangesAsync();

            return new OkResult();
        }
    }
}
