using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace A4CoreBlog.Web.Areas.Api.Controllers
{
    [Area(GlobalConstants.APIArea)]
    public class ImagesController : Controller
    {
        private readonly ISystemImageService _sysImgService;

        public ImagesController(ISystemImageService sysImgService)
        {
            _sysImgService = sysImgService;
        }

        [HttpGet]
        public IActionResult Content(int id)
        {
            var model = _sysImgService.Get<SystemImageContentViewModel>(id);
            return File(model.Content, "image/gif");
        }
    }
}
