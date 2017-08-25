using A4CoreBlog.Data.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    public class CommentController : BaseAdminController
    {
        private readonly ICommentService _comService;

        public CommentController(ICommentService comService)
        {
            _comService = comService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }
    }
}
