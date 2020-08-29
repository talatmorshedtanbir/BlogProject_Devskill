using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using BlogProject_Devskill.Web.Areas.Admin.Models.CategoryModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlogProject_Devskill.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(IConfiguration configuration, ILogger<CategoryController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<CategoryModel>();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AddCategory()
        {
            var model = new CreateCategoryModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(
          [Bind(nameof(CreateCategoryModel.Name),
            nameof(CreateCategoryModel.PostCount))] CreateCategoryModel model)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.AddAsync();
                    model.Response = new ResponseModel("Category creation successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    _logger.LogInformation(ex.Message);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Category creation failed.", ResponseType.Failure);
                    _logger.LogInformation(ex.Message);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditCategory(int id)
        {
            var model = new EditCategoryModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(
          [Bind(nameof(EditCategoryModel.Id),
            nameof(EditCategoryModel.Name),
            nameof(EditCategoryModel.PostCount))] EditCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateAsync();
                    model.Response = new ResponseModel("Category editing successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    _logger.LogInformation(ex.Message);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Category editing failed.", ResponseType.Failure);
                    _logger.LogInformation(ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new CategoryModel();
                try
                {
                    var provider = await model.DeleteAsync(id);
                    model.Response = new ResponseModel($"Category {provider} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Category deletion failed.", ResponseType.Failure);
                    _logger.LogInformation(ex.Message);
                }
            }
            return RedirectToAction("index");
        }

        public async Task<IActionResult> GetCategories()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<CategoryModel>();
            var data = await model.GetAllAsync(tableModel);
            return Json(data);
        }

    }
}
