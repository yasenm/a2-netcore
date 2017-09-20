using A4CoreBlog.Common;
using A4CoreBlog.Data.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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
        
        [HttpDelete]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Delete(int id, string type, int forId)
        {
            if (await _comService.Delete(id))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
