using Autofac;
using BlogProject_Devskill.Framework.Contexts;
using BlogProject_Devskill.Framework.Repositories;
using BlogProject_Devskill.Framework.Repositories.BlogCategoryRepos;
using BlogProject_Devskill.Framework.Repositories.CategoryRepos;
using BlogProject_Devskill.Framework.Services;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Framework.Services.PostServices;
using BlogProject_Devskill.Framework.UnitOfWorks;
using BlogProject_Devskill.Membership.Data;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework
{
    public class FrameworkModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public FrameworkModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
         
            builder.RegisterType<FrameworkContext>()
                   .WithParameter("connectionString", _connectionString)
                   .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                   .InstancePerLifetimeScope();


            builder.RegisterType<PostUnitOfWork>().As<IPostUnitOfWork>()
            .InstancePerLifetimeScope();

            builder.RegisterType<BlogUnitOfWork>().As<IBlogUnitOfWork>()
            .InstancePerLifetimeScope();


            builder.RegisterType<PostRepository>().As<IPostRepository>()
            .InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
           .InstancePerLifetimeScope();
            builder.RegisterType<BlogCategoryRepository>().As<IBlogCategoryRepository>()
          .InstancePerLifetimeScope();


            builder.RegisterType<PostService>().As<IPostService>()
              .InstancePerLifetimeScope();
            builder.RegisterType<CatergoryService>().As<ICategoryService>()
             .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUserService>().As<IApplicationUserService>()
               .InstancePerLifetimeScope();
            builder.RegisterType<CurrentUserService>().As<ICurrentUserService>()
            .InstancePerLifetimeScope();

            builder.RegisterType<AccountSeed>()
            .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
