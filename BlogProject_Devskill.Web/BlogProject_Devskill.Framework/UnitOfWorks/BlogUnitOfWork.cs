using BlogProject_Devskill.Data;
using BlogProject_Devskill.Framework.Contexts;
using BlogProject_Devskill.Framework.Repositories.CategoryRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.UnitOfWorks
{
    public class BlogUnitOfWork : UnitOfWork, IBlogUnitOfWork
    {
        //public ICategoryRepository CategoryRepository { get; set; }
        //public BlogUnitOfWork(FrameworkContext dbContext, ICategoryRepository categoryRepository) : base(dbContext)
        //{
        //    CategoryRepository = categoryRepository;
        //}
        public BlogUnitOfWork(FrameworkContext dbContext) : base(dbContext)
        {
        }
    }
}
