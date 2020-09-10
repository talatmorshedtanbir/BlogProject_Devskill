using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Services.BlogServices;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.User.Models
{
    public class CommentModel : BlogBaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int BlogId { get; set; }
        public CommentModel() : base() { }
        public CommentModel(IBlogService blogService, ICategoryService categoryService) : base(blogService, categoryService)
        {

        }
        public async Task PostComment()
        {
            var entity = new Comment
            {
                Name = this.Name,
                Email = this.Email,
                Description = this.Description,
                IsAuthorized = false,
                CommentTime = DateTime.Now
            };
            await _blogService.AddCommentAsync(entity);
        }

    }
}
