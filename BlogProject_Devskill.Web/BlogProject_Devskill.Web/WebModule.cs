using Autofac;
using BlogProject_Devskill.Membership.Data;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using BlogProject_Devskill.Web.Areas.Admin.Models.AdminControl;
using BlogProject_Devskill.Web.Areas.Admin.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public WebModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdminControlModel>();
            builder.RegisterType<EditAdminModel>();
            builder.RegisterType<CategoryModel>();
            base.Load(builder);
        }
    }
}
