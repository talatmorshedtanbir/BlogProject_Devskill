using BlogProject_Devskill.Data;
using BlogProject_Devskill.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.UnitOfWorks
{
    public interface IPostUnitOfWork : IUnitOfWork
    {
        public IPostRepository PostRepository{get;set;}
    }
}
