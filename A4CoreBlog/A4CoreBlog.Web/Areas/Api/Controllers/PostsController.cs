using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
        public IQueryable<PostListBasicViewModel> All(string authorId, int? blogId)
        {
            var model = _postService.GetAll<PostListBasicViewModel>(); //.Skip(page * size).Take(size);
            if (!string.IsNullOrEmpty(authorId))
            {
                model = model.Where(p => p.AuthorId == authorId);
            }
            if (blogId != null)
            {
                model = model.Where(p => p.BlogId == blogId);
            }
            Thread.Sleep(2000);
            return model;
        }

        [HttpGet]
        public int Count(string authorId, int? blogId)
        {
            var model = _postService.GetAll<PostListBasicViewModel>();
            if (!string.IsNullOrEmpty(authorId))
            {
                model = model.Where(p => p.AuthorId == authorId);
            }
            if (blogId != null)
            {
                model = model.Where(p => p.BlogId == blogId);
            }
            var result = model.Count();
            return result;
        }

        [HttpGet]
        public PostDetailsViewModel Get(int postId)
        {
            var model = _postService.Get<PostDetailsViewModel>(postId);
            Thread.Sleep(2000);
            return model;
        }
    }
}
