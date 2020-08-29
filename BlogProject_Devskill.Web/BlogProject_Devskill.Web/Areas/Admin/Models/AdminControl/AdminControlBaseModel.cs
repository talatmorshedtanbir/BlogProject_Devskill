using Autofac;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.AdminControl
{
    public class AdminControlBaseModel : AdminBaseModel
    {
        protected readonly IApplicationUserService _applicationUserService;
        protected readonly ICurrentUserService _currentUserService;
        public AdminControlBaseModel(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }
        public AdminControlBaseModel(ICurrentUserService currentUserService, IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
            _currentUserService = currentUserService;
        }

        public AdminControlBaseModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
            _currentUserService = Startup.AutofacContainer.Resolve<ICurrentUserService>();
        }

    }
}
