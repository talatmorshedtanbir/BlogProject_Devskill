using BlogProject_Devskill.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Services.PostServices
{
    public interface IPostService : IDisposable
    {
        Task<(IList<BlogPost> Items, int Total, int TotalFilter)> GetAllPostAsync(
         string searchText,
         string orderBy,
         int pageIndex,
         int pageSize);
        Task<BlogPost> GetByIdAsync(int id);
        Task<int> GetIdByTitleAsync(string title);
        Task AddAsync(BlogPost entity);
        Task UpdateAsync(BlogPost entity);
        Task AddBlogCategory(IList<BlogCategory> blogCategories);
        Task<BlogPost> DeleteAsync(int id);
    }
}
