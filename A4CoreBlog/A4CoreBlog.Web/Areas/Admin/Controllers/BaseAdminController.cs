using A4CoreBlog.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminArea)]
    [Authorize(Roles = GlobalConstants.TeamMemberRole)]
    public class BaseAdminController : Controller
    {
        
    }
}
