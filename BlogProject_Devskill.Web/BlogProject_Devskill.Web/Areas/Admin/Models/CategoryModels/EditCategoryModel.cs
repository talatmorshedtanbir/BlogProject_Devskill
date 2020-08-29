using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.CategoryModels
{
    public class EditCategoryModel : CategoryBaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int PostCount { get; set; }
        public EditCategoryModel(ICategoryService categoryService, IApplicationUserService applicationUserService,
        ICurrentUserService currentUserService) : base(categoryService, applicationUserService, currentUserService)
        {

        }
        public EditCategoryModel() { }

        public async Task LoadByIdAsync(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            this.Id = result.Id;
            this.Name = result.Name;
            this.PostCount = result.PostCount;
        }

        public async Task UpdateAsync()
        {
            var entity = new Category
            {
                Id = this.Id,
                Name = this.Name,
                PostCount = this.PostCount
            };
            await _categoryService.UpdateAsync(entity);
        }

    }
}
