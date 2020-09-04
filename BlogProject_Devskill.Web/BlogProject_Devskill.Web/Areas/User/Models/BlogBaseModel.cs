using Autofac;
using BlogProject_Devskill.Framework.Services;
using BlogProject_Devskill.Framework.Services.BlogServices;
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
        protected readonly IApplicationUserService _applicationUserService;
        public BlogBaseModel(IApplicationUserService applicationUserService, IBlogService blogService)
        {
            _blogService = blogService;
            _applicationUserService = applicationUserService;
        }
        public BlogBaseModel()
        {
            _blogService = Startup.AutofacContainer.Resolve<IBlogService>();
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
        }
        public void Dispose()
        {
            _blogService?.Dispose();
        }
    }
}
