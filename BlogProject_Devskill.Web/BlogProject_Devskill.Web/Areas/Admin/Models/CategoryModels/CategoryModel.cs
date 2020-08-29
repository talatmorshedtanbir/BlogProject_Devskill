using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.CategoryModels
{
    public class CategoryModel : CategoryBaseModel
    {
        public CategoryModel() : base() { }
        public CategoryModel(ICategoryService categoryService, IApplicationUserService applicationUserService,
            ICurrentUserService currentUserService) : base(categoryService, applicationUserService, currentUserService)
        {

        }
        public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _categoryService.GetAllAsync(
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name","PostCount" }),
                tableModel.PageIndex, tableModel.PageSize);
            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                                    item.Name,
                                    item.PostCount.ToString(),
                                    item.Id.ToString()
                        }
                        ).ToArray()

            };
        }
        public async Task<string> DeleteAsync(int id)
        {
            var category = await _categoryService.DeleteAsync(id);
            return category.Name;
        }
    }
}
