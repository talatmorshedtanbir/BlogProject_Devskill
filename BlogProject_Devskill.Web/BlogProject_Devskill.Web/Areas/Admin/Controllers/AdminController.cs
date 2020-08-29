using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Membership.Services;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using BlogProject_Devskill.Web.Areas.Admin.Models.AdminControl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogProject_Devskill.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(ICurrentUserService currentUserService, ILogger<AdminController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _currentUserService = currentUserService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<AdminControlModel>();
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult AddAdmin()
        {
            var model = new CreateAdminModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(
            [Bind(nameof(CreateAdminModel.FullName),
            nameof(CreateAdminModel.Email),
            nameof(CreateAdminModel.DateOfBirth),
            nameof(CreateAdminModel.PhoneNumber),
            nameof(CreateAdminModel.Gender),
            nameof(CreateAdminModel.Address))] CreateAdminModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.CreateAdminAsync();
                    model.Response = new ResponseModel("Admin Added successful.", ResponseType.Success);
                    _logger.LogInformation("New Admin Created Successfully");
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Admin added failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditAdmin(Guid id)
        {
            var model = new EditAdminModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(
            [Bind(nameof(EditAdminModel.Id),
            nameof(EditAdminModel.FullName),
            nameof(EditAdminModel.Email),
            nameof(EditAdminModel.DateOfBirth),
            nameof(EditAdminModel.PhoneNumber),
            nameof(EditAdminModel.Gender),
            nameof(EditAdminModel.Address),
            nameof(EditAdminModel.ProfileImage))] EditAdminModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ImageUrl = UploadedFile(model);
                    await model.UpdateAsync();
                    model.Response = new ResponseModel("Admin Updated successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Admin Update failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return View(model);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            if (ModelState.IsValid)
            {
                var model = new AdminControlModel();
                if (id == _currentUserService.UserId)
                {
                    model.Response = new ResponseModel($"Can not delete logged in admin", ResponseType.Failure);
                    _logger.LogInformation("Can not delete logged admin");
                    return RedirectToAction("Index");
                }
                   
                try
                {
                    var title = await model.DeleteAsync(id);
                    model.Response = new ResponseModel($"Admin {title}  successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Admin delete failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return RedirectToAction("index");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            var model = new PasswordModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await model.ChangePasswordAsync();
                    if (result == true)
                    {
                        _logger.LogInformation(" Changed Password Successfully");
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                catch
                {
                    model.Response = new ResponseModel("Failed to Change Password", ResponseType.Failure);
                    _logger.LogInformation("Failed to Change Password.");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> GetAdmins()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<AdminControlModel>();
            var data = await model.GetAllAsync(tableModel);
            return Json(data);
        }

        private string UploadedFile(EditAdminModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProfileImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminInfo(Guid id)
        {
            var model = new AdminInformationModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }
    }
}
