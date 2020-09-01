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
    public class EditPostModel : BlogPostBaseModel
    {
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
        public IList<Category> Categories { get; set; }
        public IFormFile CoverImage { get; set; }
        [Required]
        public bool UseAdminInfo { get; set; }
        public EditPostModel(ICurrentUserService currentUserService, IPostService postService,
        ICategoryService categoryService) : base(currentUserService, postService, categoryService)
        {

        }
        public EditPostModel() : base()
        {

        }

        public IList<Category> GetAllCategoryForSelectAsync()
        {
            return _categoryService.GetAllCategory();
        }

        public async Task LoadByIdAsync(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            this.Id = post.Id;
            this.AuthorId = post.AuthorId;
            this.AuthorName = post.AuthorName;
            this.Title = post.Title;
            this.Description = post.Description;
            this.UseAdminInfo = post.UseAdminInfo;
            this.CoverImageUrl = post.CoverImageUrl;
           // this.Categories = (IList<Category>)post.BlogCategories.Select(x => x.Category.Name).ToList();
            this.PublicationTime = post.CreationTime;
            this.Draft = post.Draft;
        }
        public async Task EditBlogAsync()
        {

            var blog = new BlogPost();
            blog.Id = this.Id;
            blog.AuthorId = _currentUserService.UserId;
            blog.LastEditTime = DateTime.Now;
            blog.Title = this.Title;
            blog.Description = this.Description;
            blog.CoverImageUrl = this.CoverImageUrl;
            blog.Draft = false;
            if (this.UseAdminInfo)
            {
                var author = await _applicationUserService.GetByIdAsync(_currentUserService.UserId);
                blog.AuthorName = author.FullName;
                blog.AuthorImageUrl = author.ImageUrl;
                blog.UseAdminInfo = true;
            }
            else
            {
                var defaultAuth = new DefaultValues();
                blog.AuthorName = defaultAuth.AuthorName;
                blog.AuthorImageUrl = defaultAuth.AuthorImageUrl;
                blog.UseAdminInfo = false;
            }
            await _postService.UpdateAsync(blog);

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
