using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_Devskill.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<AdminBaseModel>();
            return View(model);
        }
    }
}
