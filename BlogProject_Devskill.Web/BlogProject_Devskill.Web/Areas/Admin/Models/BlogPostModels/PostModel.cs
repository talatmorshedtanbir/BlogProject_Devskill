using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Framework.Services.PostServices;
using BlogProject_Devskill.Membership.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.BlogPostModels
{
    public class PostModel : BlogPostBaseModel
    {
        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        [StringLength(450)]
        public string Description { get; set; }

        public string CoverImageUrl { get; set; }
        [Required]
        public bool Draft { get; set; }
        public DateTime CreationTime { get; set; }
        public IList<Category> Categories { get; set; }
        public IFormFile CoverImage { get; set; }
        public PostModel(ICurrentUserService currentUserService, IPostService postService,
        ICategoryService categoryService) : base(currentUserService, postService, categoryService)
        {

        }
        public PostModel() : base()
        {

        }

        public IList<Category> GetAllCategoryForSelectAsync()
        {
            return  _categoryService.GetAllCategory();
        }

        public async Task AddBlogAsync()
        {

            var blog = new BlogPost();
            blog.AuthorId = _currentUserService.UserId;
            blog.CreationTime = DateTime.Now;
            blog.Title = this.Title;
            blog.Description = this.Description;
            blog.CoverImageUrl = this.CoverImageUrl;
            blog.Draft = false;
            await _postService.AddAsync(blog);

            var blogId = await _postService.GetIdByTitleAsync(Title);
            var blogCategories = new List<BlogCategory>();
            foreach (var item in Categories)
            {
                if (item.IsChecked)
                {
                    var newBlogCategory = new BlogCategory();
                    newBlogCategory.BlogPostId = blogId;
                    newBlogCategory.CategoryId = item.Id;
                    blogCategories.Add(newBlogCategory);
                }
            }
            await _postService.AddBlogCategory(blogCategories);
        }

    }
}
