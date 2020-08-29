using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Enums;
using BlogProject_Devskill.Framework.Services;
using BlogProject_Devskill.Framework.Services.BlogServices;
using BlogProject_Devskill.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.User.Models
{
    public class BlogModel : BlogBaseModel
    {
        public BlogModel() : base() { }
        public BlogModel(IBlogService blogService) : base(blogService)
        {

        }
        public IList<BlogPost> Blogs { get; set; }
        public Pager Pager { get; set; }
        public string Category { get; set; } // posts by category
        public PostListType PostListType { get; set; }
        //public async Task<IList<BlogPost>> GetAllAsync()
        //{
        //    var result = await _blogService.GetAllBlogAsync();
        //    return result;
        //}
        //public async Task<IList<BlogPost>> GetBlogs(Pager pager, int author = 0, string category = "", string include = "", bool sanitize = true)
        //{
        //    return  _blogService.GetBlogList( pager, author,  category , include , sanitize);
        //}
    }
}
