using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Framework.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Services.BlogServices
{
    public class BlogService : IBlogService
    {
        private IBlogUnitOfWork _blogUnitOfWork;
        public BlogService(IBlogUnitOfWork blogUnitOfWork)
        {
            _blogUnitOfWork = blogUnitOfWork;
        }
        public async Task<(IList<BlogPost> Items, int Total, int TotalFilter)> GetAllBlogAsync(
            string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<BlogPost, object>>>()
            {
                ["name"] = v => v.Title
            };

            var result = await _blogUnitOfWork.PostRepository.GetAsync<BlogPost>(
                x => x, x => x.Title.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy), x => x.Include(i => i.BlogCategories).ThenInclude(i => i.Category),
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public IList<Category> GetAllCategory()
        {
            var result = _blogUnitOfWork.CategoryRepository.GetAll();
            return result;
        }
        public BlogPost GetByIdAsync(int id)
        {
            var result = _blogUnitOfWork.PostRepository.GetById(id);

            if (result == null) throw new NotFoundException(nameof(BlogPost), id);

            return result;
        }

        public async Task<IList<BlogPost>> GetPosts()
        {
            var items = new List<BlogPost>();
            var published =  await _blogUnitOfWork.PostRepository.GetAllAsync(x=>x, x=>x.Title.Contains(""), x=>x.Include(i=>i.BlogCategories).ThenInclude(i=>i.Category));
            items = items.Concat(published).ToList();
            return items;
        }
        public async Task<IEnumerable<PostItem>> GetList(Expression<Func<BlogPost, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var items = _blogUnitOfWork.PostRepository.Get(predicate).OrderByDescending(x => x.CreationTime).ToList();
            pager.Configure(items.Count);
            var postPage = items.Skip(skip).Take(pager.ItemsPerPage).ToList();
            return await Task.FromResult(PostListToItems(postPage));
        }

        public async Task<BlogPost> GetWithIncludeByIdAsync(int id)
        {
            return await _blogUnitOfWork.PostRepository.GetFirstOrDefaultAsync(x => x, x => x.Id == id, x => x.Include(i => i.BlogCategories).ThenInclude(i => i.Category));
        }

        public List<PostItem> PostListToItems(List<BlogPost> posts)
        {
            return posts.Select(p => new PostItem
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                AuthorName = p.AuthorName,
                BlogCategories = p.BlogCategories,
                PublicationTime = p.CreationTime,
                CoverImageUrl = p.CoverImageUrl
            }).Distinct().ToList();
        }
        public IList<Comment> GetCommentsByBlogIdAsync(int id)
        {
            var result = _blogUnitOfWork.CommentRepository.Get(x=> x.BlogPostId == id && x.IsAuthorized);
            return result;
        }
        public async Task AddCommentAsync(Comment entity)
        {
            await _blogUnitOfWork.CommentRepository.AddAsync(entity);
            await _blogUnitOfWork.SaveChangesAsync();
        }
        public void Dispose()
        {
            _blogUnitOfWork.Dispose();
        }
    }
}
