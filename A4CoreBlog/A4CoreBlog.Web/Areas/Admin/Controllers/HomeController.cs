﻿using Microsoft.AspNetCore.Mvc;

namespace A4CoreBlog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
