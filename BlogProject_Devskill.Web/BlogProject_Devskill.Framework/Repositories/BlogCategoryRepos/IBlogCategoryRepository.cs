﻿using BlogProject_Devskill.Data;
using BlogProject_Devskill.Framework.Contexts;
using BlogProject_Devskill.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.Repositories.BlogCategoryRepos
{
    public interface IBlogCategoryRepository : IRepository<BlogCategory, int, FrameworkContext>
    {
    }
}
