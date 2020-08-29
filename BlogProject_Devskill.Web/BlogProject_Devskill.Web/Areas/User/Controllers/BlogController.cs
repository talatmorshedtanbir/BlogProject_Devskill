using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Services;
using BlogProject_Devskill.Framework.Services.PostServices;
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
            var model = new BlogModel();
            var pgr = new Pager(page, 10);

            //if (string.IsNullOrEmpty(term))
            //{
                   // model.Blogs = await model.GetBlogs(pgr, 0, "", "FP");
           // }
            //else
            //{
            //    model.Posts = await model.Search(pgr, term);
            //}

            if (pgr.ShowOlder) pgr.LinkToOlder = $"blog?page={pgr.Older}";
            if (pgr.ShowNewer) pgr.LinkToNewer = $"blog?page={pgr.Newer}";

            model.Pager = pgr;

            //if (!string.IsNullOrEmpty(term))
            //{
            //    model.Blogs..Title = term;
            //    model.Blog.Description = "";
            //    model.PostListType = PostListType.Search;
            //}

            return View($"~/Views/Themes/Vizew/List.cshtml", model);
        }
    }
}
