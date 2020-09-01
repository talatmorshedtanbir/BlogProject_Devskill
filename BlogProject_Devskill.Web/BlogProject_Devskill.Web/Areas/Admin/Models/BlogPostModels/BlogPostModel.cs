using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Framework.Services.PostServices;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.BlogPostModels
{
    public class BlogPostModel : BlogPostBaseModel
    {
        public IList<BlogPost> BlogPosts { get; set; }
        public BlogPostModel(ICurrentUserService currentUserService, IPostService postService,
          ICategoryService categoryService, IApplicationUserService applicationUserService) : base(currentUserService, postService, categoryService, applicationUserService)
        {

        }
        public BlogPostModel() : base()
        {
            
        }

        public async Task<object> GetAllPostAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _postService.GetAllPostAsync(
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
                            string.Join(", ",item.BlogCategories.Select(x=>x.Category.Name)),
                            item.AuthorName,
                            (item.Description.Substring(0,item.Description.Length-1>20?20:item.Description.Length-1)+".....").ToString(),
                            item.CreationTime.ToString(),
                            item.Id.ToString()
                        }).ToArray()

            };
        }

        public async Task<string> DeleteAsync(int id)
        {
            var name = await _postService.DeleteAsync(id);
            return name.Title;
        }
    }
}
