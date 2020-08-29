using Autofac;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.AdminControl
{
    public class AdminControlModel : AdminControlBaseModel
    {
        public AdminControlModel() : base() { }

        public AdminControlModel(IApplicationUserService applicationUserService) : base(applicationUserService) { }

        public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _applicationUserService.GetAllAdminAsync(
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "FullName", "Email" }),
                tableModel.PageIndex, tableModel.PageSize);

            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,
                data = (from item in result.Items
                        select new string[]
                        {
                            item.FullName,
                            item.Email,
                            item.PhoneNumber,
                            item.Id.ToString()
                        }).ToArray()
            };
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var name = await _applicationUserService.DeleteAsync(id);
            return name;
        }
    }
}
