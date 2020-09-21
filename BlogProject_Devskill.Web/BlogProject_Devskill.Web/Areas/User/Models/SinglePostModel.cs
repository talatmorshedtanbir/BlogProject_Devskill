using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Services.BlogServices;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.User.Models
{
    public class SinglePostModel : BlogBaseModel
    {
        public SinglePostModel(IBlogService blogService,
      ICategoryService categoryService) : base( blogService, categoryService)
        {

        }
        public SinglePostModel() : base()
        {

        }

        public int Id { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        [StringLength(450)]
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        [Required]
        public bool Draft { get; set; }
        public DateTime EditingTime { get; set; }
        public DateTime PublicationTime { get; set; }
        public string Categories { get; set; }
        public List<string> BlogCategories { get; set; }
        public string CommenterName { get; set; }
        public string CommenterEmail { get; set; }
        public string CommentDescription { get; set; }
        public IList<Comment> Comments { get; set; }
        public async Task LoadByIdAsync(int id)
        {
            var post = await _blogService.GetWithIncludeByIdAsync(id);
            this.Id = post.Id;
            this.AuthorName = post.AuthorName;
            this.Title = post.Title;
            this.Description = post.Description;
            this.CoverImageUrl = post.CoverImageUrl;
            this.AuthorImageUrl = post.AuthorImageUrl;
            this.Categories = string.Join(", ", post.BlogCategories.Select(x => x.Category.Name));
            this.BlogCategories = post.BlogCategories.Select(x => x.Category.Name).ToList();
            this.PublicationTime = post.CreationTime;
        }

        public void LoadCommentsByBlogIdAsync(int id)
        {
            this.Comments = _blogService.GetCommentsByBlogIdAsync(id);
        }
        public async Task LoadCommentsByIdAsync(int id)
        {
            var post = await _blogService.GetWithIncludeByIdAsync(id);
        }
    }
}
