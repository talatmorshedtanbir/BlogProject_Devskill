using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using BlogProject_Devskill.Web;
using BlogProject_Devskill.Framework.Extensions;
using BlogProject_Devskill.Framework.MenuItems;
using BlogProject_Devskill.Membership.Services;

namespace BlogProject_Devskill.Web.Areas.Admin.Models
{
    public abstract class AdminBaseModel
    {
        public MenuModel MenuModel { get; set; }
        public ResponseModel Response
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.IsAvailable &&
                    _httpContextAccessor.HttpContext.Session.Keys.Contains(nameof(Response)))
                {
                    var response = _httpContextAccessor.HttpContext.Session.Get<ResponseModel>(nameof(Response));
                    _httpContextAccessor.HttpContext.Session.Remove(nameof(Response));

                    return response;
                }
                else
                    return null;
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.Set(nameof(Response), value);
            }
        }

        protected IHttpContextAccessor _httpContextAccessor;
        protected ICurrentUserService _currentUserService;
        protected IApplicationUserService _applicationuserService;
        public AdminBaseModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            SetupMenu();
        }

        public AdminBaseModel(ICurrentUserService currentUserService, IApplicationUserService applicationuserService)
        {
            _applicationuserService = applicationuserService;
            _currentUserService = currentUserService;
            SetupMenu();
        }
        public AdminBaseModel()
        {
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
            _applicationuserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
            SetupMenu();
        }
        private void SetupMenu()
        {
            MenuModel = new MenuModel
            {
                MenuItems = new List<MenuItem>
                {
                    {
                        new MenuItem
                        {
                            Title = "Profile",
                            Icon = "fas fa-user-tie",
                            Childs = new List<MenuChildItem>
                            {
                                 new MenuChildItem{ Title = "View Informations", Url = $"/Admin/Admin/AdminInfo/{_currentUserService.UserId}", Icon = "fas fa-id-card" },
                                 new MenuChildItem{ Title = "Edit Informations", Url = $"/Admin/Admin/EditAdmin/{_currentUserService.UserId}", Icon = "	fas fa-user-edit" },
                                 new MenuChildItem{ Title = "Change Password", Url = "/Admin/Admin/ChangePassword", Icon = "fa fa-asterisk" }

                            }

                        }

                    },
                    {
                        new MenuItem
                        {
                            Title = "Category",
                            Icon = "fas fa-edit",
                            Childs = new List<MenuChildItem>
                            {
                                new MenuChildItem{ Title = "View Categories", Url = "/Admin/Category/Index", Icon="fas fa-clipboard" },
                                 new MenuChildItem{ Title = "Add New Category", Url = "/Admin/Category/AddCategory", Icon="fas fa-edit" }
                            }

                        }
                    }
                }
            };
        }

    }
}
