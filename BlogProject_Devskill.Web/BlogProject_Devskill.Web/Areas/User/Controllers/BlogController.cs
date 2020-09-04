using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Services;
using BlogProject_Devskill.Framework.Services.PostServices;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using BlogProject_Devskill.Web.Areas.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_Devskill.Web.Areas.User.Controllers
{
    [Area("User")]
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        public BlogController(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<IActionResult> Index(string term, int page = 1)
        {
             var model1 = Startup.AutofacContainer.Resolve<BlogModel>();
            // var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new ListModel
            {
                PostListType = PostListType.Blog
            };
            var pgr = new Pager(page, 10);
            model.Posts = await model1.GetList(pgr,"");
            if (pgr.ShowOlder) pgr.LinkToOlder = $"blog?page={pgr.Older}";
            if (pgr.ShowNewer) pgr.LinkToNewer = $"blog?page={pgr.Newer}";
            model.Pager = pgr;
            if (!string.IsNullOrEmpty(term))
            {
                model.Blog.Title = term;
                model.Blog.Description = "";
            }
            return View(model);
        }
    }
}
