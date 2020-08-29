using BlogProject_Devskill.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Services.BlogServices
{
    public interface IBlogService : IDisposable
    {
        Task<(IList<BlogPost> Items, int Total, int TotalFilter)> GetAllBlogAsync(
                string searchText,
                string orderBy,
                int pageIndex,
                int pageSize);
        BlogPost GetByIdAsync(int id);
       Task<IEnumerable<BlogPost>> GetBlogList(Pager pager, int author, string category,  string include , bool sanitize );
    }
}
