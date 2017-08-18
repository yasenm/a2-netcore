using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace A4CoreBlog.Web.Areas.Api.Controllers
{
    public class BlogsController : BaseApiAreaController
    {
        private IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet()]
        public IEnumerable<BasicBlogViewModel> All()
        {
            var result = _blogService.GetAll<BasicBlogViewModel>().ToList();
            Thread.Sleep(2000);
            return result;
        }

        [HttpGet()]
        public BasicBlogViewModel Details(string userId)
        {
            var result = _blogService.Get<BasicBlogViewModel>(userId);
            Thread.Sleep(2000);
            return result;
        }
    }
}
