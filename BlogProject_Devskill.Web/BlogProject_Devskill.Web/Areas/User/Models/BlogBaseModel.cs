using Autofac;
using BlogProject_Devskill.Framework.Services;
using BlogProject_Devskill.Framework.Services.BlogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.User.Models
{
    public class BlogBaseModel :  IDisposable
    {
        protected readonly IBlogService _blogService;
        public BlogBaseModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public BlogBaseModel()
        {
            _blogService = Startup.AutofacContainer.Resolve<IBlogService>();
        }
        public void Dispose()
        {
            _blogService?.Dispose();
        }
    }
}
