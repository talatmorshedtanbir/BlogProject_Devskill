using BlogProject_Devskill.Data;
using BlogProject_Devskill.Framework.Contexts;
using BlogProject_Devskill.Framework.Repositories;
using BlogProject_Devskill.Framework.Repositories.BlogCategoryRepos;
using BlogProject_Devskill.Framework.Repositories.BlogRepos;
using BlogProject_Devskill.Framework.Repositories.CategoryRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.UnitOfWorks
{
    public class BlogUnitOfWork : UnitOfWork, IBlogUnitOfWork
    {
        public IPostRepository PostRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IBlogCategoryRepository BlogCategoryRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public BlogUnitOfWork(FrameworkContext context, IPostRepository postRepository, ICategoryRepository categoryRepository,
            IBlogCategoryRepository blogCategoryRepository, ICommentRepository commentRepository)
          : base(context)
        {
            PostRepository = postRepository;
            CategoryRepository = categoryRepository;
            BlogCategoryRepository = blogCategoryRepository;
            CommentRepository = commentRepository;
        }
    }
}
