using BlogProject_Devskill.Data;
using BlogProject_Devskill.Framework.Contexts;
using BlogProject_Devskill.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.UnitOfWorks
{
    public class PostUnitOfWork : UnitOfWork, IPostUnitOfWork
    {
        public IPostRepository PostRepository { get; set; }
        public PostUnitOfWork(FrameworkContext context, IPostRepository postRepository)
          : base(context)
        {
            PostRepository = postRepository;
        }
    }
}
