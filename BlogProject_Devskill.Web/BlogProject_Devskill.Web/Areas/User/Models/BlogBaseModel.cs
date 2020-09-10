using Autofac;
using BlogProject_Devskill.Framework.Services;
using BlogProject_Devskill.Framework.Services.BlogServices;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Membership.Services;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.User.Models
{
    public class BlogBaseModel : AdminBaseModel,  IDisposable
    {
        protected readonly IBlogService _blogService;
        protected readonly ICategoryService _categoryService;
        public BlogBaseModel(IBlogService blogService,ICategoryService categoryService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }
        public BlogBaseModel()
        {
            _blogService = Startup.AutofacContainer.Resolve<IBlogService>();
            _categoryService = Startup.AutofacContainer.Resolve<ICategoryService>();
        }
        public void Dispose()
        {
            _blogService?.Dispose();
        }
    }
}
