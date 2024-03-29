﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BlogProject_Devskill.Web.Areas.Admin.Models.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_Devskill.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Administrator")]
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<DashboardModel>();
            return View(model);
        }
    }
}
