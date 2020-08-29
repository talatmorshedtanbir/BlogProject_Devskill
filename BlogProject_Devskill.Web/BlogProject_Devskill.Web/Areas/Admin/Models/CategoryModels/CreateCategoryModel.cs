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
    public class CreateCategoryModel : CategoryBaseModel
    {
        [Required]
        public string Name { get; set; }
        public int PostCount { get; set; }
        public CreateCategoryModel(ICategoryService categoryService, IApplicationUserService applicationUserService,
        ICurrentUserService currentUserService) : base(categoryService, applicationUserService, currentUserService)
        {

        }

        public CreateCategoryModel() : base() { }

        public async Task AddAsync()
        {

            var entity = new Category
            {
                Name = this.Name,
                PostCount = 0

            };
            await _categoryService.AddAsync(entity);
        }
    }
}
