using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using A4CoreBlog.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    public class BlogController : BaseAdminController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _blogService.Get<BlogEditViewModel>(id);
            if (User.GetUserId() == model.OwnerId)
            {
                return View(model);
            }
            return RedirectToAction(nameof(Index), "blog", new { @area = GlobalConstants.AdminArea });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogEditViewModel model)
        {
            var blogDb = _blogService.Get<BlogEditViewModel>(model.Id);
            if (blogDb.OwnerId != HttpContext.User.GetUserId())
            {
                return RedirectToAction(nameof(Index), "blog", new { @area = GlobalConstants.AdminArea });
            }
            if (ModelState.IsValid)
            {
                var success = await _blogService.Edit(model);
                if (success)
                {
                    return RedirectToAction(nameof(Index), "blog", new { @area = GlobalConstants.AdminArea });
                }
            }
            return View(model);
        }
    }
}
