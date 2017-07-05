using A4CoreBlog.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminArea)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            _logger.LogInformation($"{nameof(Index)} called from - {nameof(HomeController)}");
            return View();
        }
    }
}
