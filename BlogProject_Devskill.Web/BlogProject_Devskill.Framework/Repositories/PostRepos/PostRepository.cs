using BlogProject_Devskill.Data;
using BlogProject_Devskill.Framework.Contexts;
using BlogProject_Devskill.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.Repositories
{
    public class PostRepository : Repository<BlogPost, int, FrameworkContext>, IPostRepository
    {
        public PostRepository(FrameworkContext databaseContext) : base(databaseContext)
        {

        }
    }
}
