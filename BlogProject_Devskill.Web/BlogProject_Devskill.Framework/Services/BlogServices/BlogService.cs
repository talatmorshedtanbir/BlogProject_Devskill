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
        private IPostUnitOfWork _postUnitOfWork;
        public BlogService(IPostUnitOfWork postUnitOfWork)
        {
            _postUnitOfWork = postUnitOfWork;
        }
        public async Task<(IList<BlogPost> Items, int Total, int TotalFilter)> GetAllBlogAsync(
                string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<BlogPost, object>>>()
            {
                ["Title"] = v => v.Title
            };
            var result = await _postUnitOfWork.PostRepository.GetAsync<BlogPost>(
                x => x, x =>  (x.Title.Contains(searchText)),
                x => x.ApplyOrdering(columnsMap, orderBy),
                pageIndex, pageSize, true);
            return (result.Items, result.Total, result.TotalFilter);
        }
        public BlogPost GetByIdAsync(int id)
        {
            var result = _postUnitOfWork.PostRepository.GetById(id);

            if (result == null) throw new NotFoundException(nameof(BlogPost), id);

            return result;
        }

        public async Task<IEnumerable<BlogPost>> GetBlogList(Pager pager, int author = 0, string category = "", string include = "", bool sanitize = true)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;
            var Post = new List<BlogPost>();
            var posts = _postUnitOfWork.PostRepository.GetAll();
            foreach(var post in posts)
            {
                if (string.IsNullOrEmpty(category))
                {
                    Post.Add(post);
                }
                else
                {
                    if (!string.IsNullOrEmpty(post.Categories))
                    {
                        var cats = post.Categories.ToLower().Split(',');
                        if (cats.Contains(category.ToLower()))
                        {
                            Post.Add(post);
                        }
                    }
                }
            }
            pager.Configure(posts.Count);
            var items = new List<BlogPost>();
            foreach (var p in posts.Skip(skip).Take(pager.ItemsPerPage).ToList())
            {
                items.Add(p);
            }
            return items;
        }
        public void Dispose()
        {
            _postUnitOfWork.Dispose();
        }
    }
}
