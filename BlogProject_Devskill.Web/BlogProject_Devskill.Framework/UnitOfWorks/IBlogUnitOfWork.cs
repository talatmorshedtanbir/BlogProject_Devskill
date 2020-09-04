using BlogProject_Devskill.Data;
using BlogProject_Devskill.Framework.Repositories;
using BlogProject_Devskill.Framework.Repositories.BlogCategoryRepos;
using BlogProject_Devskill.Framework.Repositories.CategoryRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.UnitOfWorks
{
    public interface IBlogUnitOfWork : IUnitOfWork
    {
        public IPostRepository PostRepository { get; set; }
        public IBlogCategoryRepository BlogCategoryRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
    }
}
