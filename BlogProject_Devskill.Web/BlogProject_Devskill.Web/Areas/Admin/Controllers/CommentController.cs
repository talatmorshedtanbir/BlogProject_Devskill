using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using BlogProject_Devskill.Web.Areas.Admin.Models.BlogPostModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlogProject_Devskill.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CommentController> _logger;
        public CommentController(IConfiguration configuration, ILogger<CommentController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index(int id)
        {
            var model = Startup.AutofacContainer.Resolve<CommentModel>();
            model.BlogId = id;
            return View(model);
        }

        public async Task<IActionResult> GetCommentsById(int id)
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<CommentModel>();
            var data = await model.GetCommentsByIdAsync(tableModel,id);
            return Json(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveComment(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new CommentModel();
                await model.LoadByIdAsync(id);
                try
                {
                    if(model.IsAuthorized)
                    {
                        await model.ApproveAsync(id, false);
                        model.Response = new ResponseModel($"Comment was successfully disapproved.", ResponseType.Success);
                    }
                    else
                    {
                        await model.ApproveAsync(id, true);
                        model.Response = new ResponseModel($"Comment was successfully approved.", ResponseType.Success);
                    }
                    return RedirectToAction("Index","BlogPost");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Comment approvation failed.", ResponseType.Failure);
                    _logger.LogInformation(ex.Message);
                }
            }
            return RedirectToAction("Index","BlogPost");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ViewComment(int id)
        {
            var model = new CommentModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new CommentModel();
                try
                {
                    var provider = await model.DeleteAsync(id);
                    model.Response = new ResponseModel($"Comment {provider} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index", "BlogPost");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Comment deletion failed.", ResponseType.Failure);
                    _logger.LogInformation(ex.Message);
                }
            }
            return RedirectToAction("Index", "BlogPost");
        }
    }
}
