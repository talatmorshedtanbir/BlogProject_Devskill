using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using BlogProject_Devskill.Web.Areas.Admin.Models.BlogPostModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlogProject_Devskill.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BlogPostController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogPostController(IConfiguration configuration, ILogger<BlogPostController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<BlogPostModel>();
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> AddBlog()
        {
            var model = new PostModel();
            model.Categories = model.GetAllCategoryForSelectAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(PostModel model)
        {
            model.CoverImageUrl = UploadedFile(model);
            if (ModelState.IsValid)
            {
               
                try
                {
                    await model.AddBlogAsync();
                    var msg = "Congrats! Added Blog Successfully";
                    _logger.LogInformation("Blog Added Successfully");
                    model.Response = new ResponseModel(msg, ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var msg = "Failed to Add Blog";
                    model.Response = new ResponseModel(msg, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }
            model.Categories = model.GetAllCategoryForSelectAsync();
            return View(model);
        }

        public async Task<IActionResult> GetAllPosts()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<BlogPostModel>();
            var data = await model.GetAllPostAsync(tableModel);
            return Json(data);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditBlog(int id)
        {
            var model = new EditPostModel();
            await model.LoadByIdAsync(id);
            model.Categories = model.GetAllCategoryForSelectAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(EditPostModel model)
        {
            var newUrl = UploadedFile(model);
            if (ModelState.IsValid)
            {
                try
                {
                    await model.EditBlogAsync(newUrl);
                    var msg = "Congrats! Editted Blog Successfully";
                    _logger.LogInformation("Blog Editted Successfully");
                    model.Response = new ResponseModel(msg, ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var msg = "Failed to Edit Blog";
                    model.Response = new ResponseModel(msg, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }
            model.Categories = model.GetAllCategoryForSelectAsync();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new BlogPostModel();
                try
                {
                    var title = await model.DeleteAsync(id);
                    model.Response = new ResponseModel($"Blog {title} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Blog deletion failed.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return RedirectToAction("Index");
        }
        private string UploadedFile(PostModel model)
        {
            string uniqueFileName = "defaultIcon.jpg";

            if (model.CoverImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "CoverImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CoverImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private string UploadedFile(EditPostModel model)
        {
            string uniqueFileName = "defaultIcon.jpg";

            if (model.CoverImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "CoverImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_"+ model.CoverImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> BlogInformation(int id)
        {
            var model = new BlogInformationModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }
    }
}
