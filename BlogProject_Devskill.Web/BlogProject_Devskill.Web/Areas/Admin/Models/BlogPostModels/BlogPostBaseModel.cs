using Autofac;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Framework.Services.PostServices;
using BlogProject_Devskill.Membership.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.BlogPostModels
{
    public class BlogPostBaseModel : AdminBaseModel, IDisposable
    {
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IApplicationUserService _applicationUserService;
        protected readonly IPostService _postService;
        protected readonly ICategoryService _categoryService;
        public BlogPostBaseModel(ICurrentUserService currentUserService, IPostService postService,
            ICategoryService categoryService,  IApplicationUserService applicationUserService)
        {
            _currentUserService = currentUserService;
            _postService = postService;
            _categoryService = categoryService;
            _applicationUserService = applicationUserService;
        }
        public BlogPostBaseModel(ICurrentUserService currentUserService, IPostService postService,
          ICategoryService categoryService)
        {
            _currentUserService = currentUserService;
            _postService = postService;
            _categoryService = categoryService;
        }
        public BlogPostBaseModel()
        {
            _categoryService = Startup.AutofacContainer.Resolve<ICategoryService>();
            _postService = Startup.AutofacContainer.Resolve<IPostService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
        }
        public void Dispose()
        {
            _categoryService?.Dispose();
            _postService?.Dispose();
        }
    }
}
