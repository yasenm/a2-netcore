using A4CoreBlog.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            _logger.LogInformation($"{nameof(Index)} called from - {nameof(HomeController)}");
            return View();
        }
    }
}
