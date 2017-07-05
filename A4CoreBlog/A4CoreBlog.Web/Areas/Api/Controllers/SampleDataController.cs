using A4CoreBlog.Data.Models;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A4CoreBlog.Web.Areas.Api.Controllers
{
    [Area("api")]
    public class SampleDataController : Controller
    {
        private IBlogService _blogService;

        public SampleDataController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet()]
        public IEnumerable<BasicBlogViewModel> All()
        {
            var result = _blogService.GetAll<BasicBlogViewModel>().ToList();
            return result;
        }
    }
}
