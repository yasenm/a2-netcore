using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Api.Controllers
{
    [Area(GlobalConstants.APIArea)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IEnumerable<PostListBasicViewModel> All(string authorId, int? blogId)
        {
            var model = _postService.GetAll<PostListBasicViewModel>();
            if (!string.IsNullOrEmpty(authorId))
            {
                model = model.Where(p => p.AuthorId == authorId).ToList();
            }
            if (blogId != null)
            {
                model = model.Where(p => p.BlogId == blogId).ToList();
            }
            return model;
        }
        
        [HttpGet]
        public PostDetailsViewModel Get(int postId)
        {
            var model = _postService.Get<PostDetailsViewModel>(postId);
            return model;
        }
    }
}
