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
using Microsoft.Extensions.Logging;

namespace BlogProject_Devskill.Web.Areas.User.Controllers
{
    [Area("User")]
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        private readonly ILogger<BlogController> _logger;
        public BlogController(IPostService postService, ILogger<BlogController> logger)
        {
            _postService = postService;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string term, int page = 1)
        {
            var model = Startup.AutofacContainer.Resolve<BlogModel>();
            var listModel = new ListModel
            {
                PostListType = PostListType.Blog
            };
            var pgr = new Pager(page, 10);
            listModel.Posts = await model.GetList(pgr,"");
            if (pgr.ShowOlder) pgr.LinkToOlder = $"blog?page={pgr.Older}";
            if (pgr.ShowNewer) pgr.LinkToNewer = $"blog?page={pgr.Newer}";
            model.Pager = pgr;
            if (!string.IsNullOrEmpty(term))
            {
                listModel.Blog.Title = term;
                listModel.Blog.Description = "";
                listModel.PostListType = PostListType.Search;
            }
            return View(listModel);
        }

        [HttpGet]
        public async Task<IActionResult> SingleBlog(int id)
        {
            var model = new SinglePostModel();
            await model.LoadByIdAsync(id);
            model.LoadCommentsByBlogIdAsync(id);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(SinglePostModel model)
        {
            var commentModel = new CommentModel();
            commentModel.BlogId = model.Id;
            commentModel.Name = model.CommenterName;
            commentModel.Email = model.CommenterEmail;
            commentModel.Description = model.CommentDescription;
            try
            {
                await commentModel.PostComment();
                _logger.LogInformation("Comment Added Successfully");
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            
            return RedirectToAction("SingleBlog", "Blog", new { @id = model.Id });
        }


        //[HttpGet("categories/{id}")]
        //public async Task<IActionResult> Categories(string name, int page = 1)
        //{
        //    var model = Startup.AutofacContainer.Resolve<BlogModel>();
        //    var listModel = new ListModel
        //    {
        //        PostListType = PostListType.Blog
        //    };
        //    var pgr = new Pager(page, 10);

        //    listModel.Posts = await model.GetList(p => p.BlogCategories.Contains(), pgr);

        //    if (pgr.ShowOlder) pgr.LinkToOlder = $"?page={pgr.Older}";
        //    if (pgr.ShowNewer) pgr.LinkToNewer = $"?page={pgr.Newer}";

        //    model.Pager = pgr;

        //    return View($"~/Views/Themes/{blog.Theme}/List.cshtml", model);
        //}
    }
}
