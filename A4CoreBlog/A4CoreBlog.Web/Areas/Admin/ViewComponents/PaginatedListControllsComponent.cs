using A4CoreBlog.Data.Infrastructure;
using A4CoreBlog.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace A4CoreBlog.Web.Areas.Admin.ViewComponents
{
    public class PaginatedListControllsComponent : ViewComponent
    {
        public PaginatedListControllsComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(BasePaginatedList list)
        {
            return View("Default", list);
        }
    }
}
