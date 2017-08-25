using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Api.Controllers
{
    public class CommentsController : BaseApiAreaController
    {
        private readonly ICommentService _comService;

        public CommentsController(ICommentService comService)
        {
            _comService = comService;
        }

        [HttpGet]
        public async Task<IActionResult> ForBlog(int blogId)
        {
            var result = await _comService.GetAllForBlog<BaseCommentViewModel>(blogId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ForPost(int postId)
        {
            var result = await _comService.GetAllForPost<BaseCommentViewModel>(postId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPost([FromBody]PostACommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.AuthorName = User.GetJwtTokenUserName();
                var result = await _comService.AddPostComment(model);
                if (result > 0)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> AddToBlog(PostACommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.AuthorName = User.GetJwtTokenUserName();
                var result = await _comService.AddBlogComment(model);
                if (result > 0)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }
    }
}
