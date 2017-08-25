using A4CoreBlog.Common;
using A4CoreBlog.Data.Infrastructure;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    public class PostController : BaseAdminController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> MyPosts(int? page)
        {
            var model = _postService.GetAll<PostListBasicViewModel>()
                .Where(p => p.AuthorId == User.GetUserId())
                .OrderBy(p => p.BlogId);

            var result = await PaginatedList<PostListBasicViewModel>.CreateAsync(model, page ?? 1, 5);

            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _postService.Get<PostEditViewModel>(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new PostEditViewModel();
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_postService.AddOrUpdate(model))
                {
                    return RedirectToAction("MyPosts", new { @area = GlobalConstants.AdminArea });
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.AuthorId = HttpContext.User.GetUserId();
                if (_postService.AddOrUpdate(model))
                {
                    return RedirectToAction("MyPosts", new { @area = GlobalConstants.AdminArea });
                }
            }
            return View("Edit", model);
        }
    }
}
