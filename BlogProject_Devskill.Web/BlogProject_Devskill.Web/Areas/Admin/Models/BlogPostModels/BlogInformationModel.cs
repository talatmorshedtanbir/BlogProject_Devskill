using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Framework.Services.PostServices;
using BlogProject_Devskill.Membership.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.BlogPostModels
{
    public class BlogInformationModel : BlogPostBaseModel
    {
        public BlogInformationModel(ICurrentUserService currentUserService, IPostService postService,
        ICategoryService categoryService) : base(currentUserService, postService, categoryService)
        {

        }
        public BlogInformationModel() : base()
        {

        }

        public int Id { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
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
        public IFormFile CoverImage { get; set; }


        public async Task LoadByIdAsync(int id)
        {
            var post = await _postService.GetWithIncludeByIdAsync(id);
            this.Id = post.Id;
            this.AuthorId = post.AuthorId;
            this.AuthorName = post.AuthorName;
            this.Title = post.Title;
            this.Description = post.Description;
            this.CoverImageUrl = post.CoverImageUrl;
            this.Categories = string.Join(", ", post.BlogCategories.Select(x=> x.Category.Name));
            this.PublicationTime = post.CreationTime;
            this.EditingTime = post.LastEditTime;
            this.Draft = post.Draft;
        }
    }
}
