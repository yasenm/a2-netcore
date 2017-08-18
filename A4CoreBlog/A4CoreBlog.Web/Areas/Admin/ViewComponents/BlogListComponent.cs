using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Admin.ViewComponents
{
    public class BlogListComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public BlogListComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _blogService.GetAll<BasicBlogViewModel>();
            return View("Default", model);
        }
    }
}
