using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogProject_Devskill.Membership.Entities;
using BlogProject_Devskill.Membership.Services;
using BlogProject_Devskill.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace BlogProject_Devskill.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _loginLogger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationRoleManager _roleManager;

        public AccountsController(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> loginLogger,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager,
            ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loginLogger = loginLogger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> Login(string returnUrl)
        {
            var model = new LoginModel();

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _loginLogger.LogInformation("Admin logged in.");

                var user = await _userManager.FindByNameAsync(model.UserName);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles[0] == "Administrator" || roles[0] == "SuperAdmin")
                {
                    return LocalRedirect("~/Admin/Dashboard/Index");
                }
                else
                {
                    await _signInManager.SignOutAsync();
                }
                return LocalRedirect(model.ReturnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
            }
            if (result.IsLockedOut)
            {
                _loginLogger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

        }


    }
}
