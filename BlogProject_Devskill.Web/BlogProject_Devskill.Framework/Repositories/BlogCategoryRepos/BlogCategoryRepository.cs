using BlogProject_Devskill.Data;
using BlogProject_Devskill.Framework.Contexts;
using BlogProject_Devskill.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.Repositories.BlogCategoryRepos
{
    public class BlogCategoryRepository : Repository<BlogCategory, int, FrameworkContext>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(FrameworkContext databaseContext) : base(databaseContext)
        {

        }
    }
}
