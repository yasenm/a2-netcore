using A4CoreBlog.Common;
using A4CoreBlog.Data.Infrastructure;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    public class ImageController : BaseAdminController
    {
        private readonly ISystemImageService _sysImgService;

        public ImageController(ISystemImageService sysImgService)
        {
            _sysImgService = sysImgService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var model = _sysImgService.GetCollection<AdminListImageViewModel>();

            var result = await PaginatedList<AdminListImageViewModel>.CreateAsync(model, page ?? 1, 12);
            return View(result);
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
                    return RedirectToAction("Index", "Image", new { @area = GlobalConstants.AdminArea });
                }
            }

            return View(model);
        }
    }
}
