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
        public async Task<IActionResult> Index(string searchString="", int currentPage = 1)
        {
            var model = Startup.AutofacContainer.Resolve<BlogModel>();
            var listModel = new ListModel
            {
                PostListType = PostListType.Blog
            };
            var pgr = new Pager(currentPage, 10);
            var result = await model.GetList(pgr,"");
            listModel.Posts = result.Item1;
            listModel.TotalPages = (int)Math.Ceiling(decimal.Divide(result.Item2, 10));
            listModel.Posts = listModel.Posts.Where(x => x.Title.Contains(searchString));
            if (pgr.ShowOlder) pgr.LinkToOlder = $"blog?currentPage={pgr.Older}";
            if (pgr.ShowNewer) pgr.LinkToNewer = $"blog?currentPage={pgr.Newer}";
            listModel.Pager = pgr;
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

        [HttpPost]
        public IActionResult Search(string term)
        {
            return Redirect($"/User/blog?searchString={term}");
        }
    }
}
