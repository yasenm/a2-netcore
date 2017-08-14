using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    public class ImageController : BaseAdminController
    {
        private readonly ISystemImageService _sysImgService;

        public ImageController(ISystemImageService sysImgService)
        {
            _sysImgService = sysImgService;
        }

        public IActionResult Index()
        {
            var model = _sysImgService.GetCollection<AdminListImageViewModel>();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new SystemImageCreateOrEditViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SystemImageCreateOrEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.ImageRawContent.CopyTo(memoryStream);
                    model.Content = memoryStream.ToArray();
                }
                model = _sysImgService.AddOrUpdate(model);
                if (model.Id != 0)
                {
                    return RedirectToAction("Content", "Images", new { @area = GlobalConstants.APIArea, id = model.Id });
                }
            }

            return View(model);
        }
    }
}
