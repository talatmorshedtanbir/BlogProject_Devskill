using BlogProject_Devskill.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Services.CommentService
{   public interface ICommentService : IDisposable
    {
        Task<(IList<Comment> Items, int Total, int TotalFilter)> GetCommentsByIdAsync(
            int searchId,
            string searchText,
            string orderBy,
            int pageIndex,
            int pageSize);
        Task<Comment> GetByIdAsync(int id);
        Task UpdateAsync(Comment entity);
        Task<Comment> DeleteAsync(int id);
    }
}
