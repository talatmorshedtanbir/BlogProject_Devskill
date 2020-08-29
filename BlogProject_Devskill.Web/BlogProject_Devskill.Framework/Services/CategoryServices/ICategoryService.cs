using BlogProject_Devskill.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Services.CategoryServices
{
    public interface ICategoryService : IDisposable
    {
        Task<(IList<Category> Items, int Total, int TotalFilter)> GetAllAsync(
       string searchText,
       string orderBy,
       int pageIndex,
       int pageSize);

        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task<Category> DeleteAsync(int id);
    }
}
