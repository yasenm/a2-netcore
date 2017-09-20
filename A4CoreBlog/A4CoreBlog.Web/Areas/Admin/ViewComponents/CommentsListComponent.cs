using A4CoreBlog.Data.Services.Contracts;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Admin.ViewComponents
{
    public class CommentsListComponent : ViewComponent
    {
        private readonly ICommentService _commentService;

        public CommentsListComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(string type, int forId)
        {
            if (type == "post")
            {
                var result = await _commentService.GetAllForPost<BaseCommentViewModel>(forId);
                return View("Default", result.ToList());
            }
            else if (type == "blog")
            {
                var result = await _commentService.GetAllForBlog<BaseCommentViewModel>(forId);
                return View("Default", result.ToList());
            }
            return View<ICollection<BaseCommentViewModel>>("Default", null);
        }
    }
}
