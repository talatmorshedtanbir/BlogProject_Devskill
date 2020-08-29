using Autofac;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.CategoryModels
{
    public class CategoryBaseModel : AdminBaseModel, IDisposable
    {
        protected readonly ICategoryService _categoryService;
        protected readonly IApplicationUserService _applicationUserService;
        protected readonly ICurrentUserService _currentUserService;
        public CategoryBaseModel()
        {
            _categoryService = Startup.AutofacContainer.Resolve<ICategoryService>();
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }
        public CategoryBaseModel(ICategoryService categoryService, IApplicationUserService applicationUserService,
            ICurrentUserService currentUserService)
        {
            _categoryService = categoryService;
            _applicationUserService = applicationUserService;
            _currentUserService = currentUserService;
        }
        public void Dispose()
        {
            _categoryService?.Dispose();
        }
    }
}
