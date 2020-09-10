using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Enums;
using BlogProject_Devskill.Framework.Services;
using BlogProject_Devskill.Framework.Services.BlogServices;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Membership.Services;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.User.Models
{
    public class BlogModel : BlogBaseModel
    {
        public BlogModel() : base() { }
        public BlogModel( IBlogService blogService, ICategoryService categoryService) : base( blogService, categoryService)
        {

        }
        public IList<BlogPost> Blogs { get; set; }
        public BlogPost Blog { get; set; }
        public Pager Pager { get; set; }
        public string Category { get; set; }
        public async Task<object> GetAllBlogAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _blogService.GetAllBlogAsync(
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Title" }),
                tableModel.PageIndex, tableModel.PageSize);

            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                            item.Title,
                            string.Join(", ", item.BlogCategories.Select(x => x.Category.Name)),
                            item.AuthorName,
                            (item.Description.Substring(0, item.Description.Length - 1 > 20 ? 20 : item.Description.Length - 1) + ".....").ToString(),
                            item.CreationTime.ToString(),
                            item.Id.ToString()
                        }).ToArray()

            };
        }

        public async Task<IList<BlogPost>> GetList(Pager pager , string category = "", bool sanitize = true)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var posts = new List<BlogPost>();
            var blogs = await _blogService.GetPosts();
            foreach (var p in blogs)
            {
                posts.Add(p);
            }
            pager.Configure(posts.Count);

            var items = new List<BlogPost>();
            foreach (var p in posts.Skip(skip).Take(pager.ItemsPerPage).ToList())
            {
                items.Add(p);
            }
            return await Task.FromResult(items);
        }

        public async Task<IList<PostItem>> GetList(Expression<Func<BlogPost, bool>> predicate, Pager pager)
        {
            var skip = pager.CurrentPage * pager.ItemsPerPage - pager.ItemsPerPage;

            var posts = new List<PostItem>();
            var blogs = await _blogService.GetList(predicate, pager);
            foreach (var p in blogs)
            {
                posts.Add(p);
            }
            pager.Configure(posts.Count);

            var items = new List<PostItem>();
            foreach (var p in posts.Skip(skip).Take(pager.ItemsPerPage).ToList())
            {
                items.Add(p);
            }
            return await Task.FromResult(items);
        }
    }
}
