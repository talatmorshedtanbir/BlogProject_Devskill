using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Framework.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Services.CommentService
{
    public class CommentService : ICommentService, IDisposable
    {
        public IPostUnitOfWork _postUnitOfWork { get; set; }
        public CommentService(IPostUnitOfWork postUnitOfWork)
        {
            _postUnitOfWork = postUnitOfWork;
        }
        public async Task<(IList<Comment> Items, int Total, int TotalFilter)> GetCommentsByIdAsync(
         int searchId, string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Comment, object>>>()
            {
                ["name"] = v => v.Name
            };

            var result = await _postUnitOfWork.CommentRepository.GetAsync<Comment>(
                x => x, x => x.BlogPostId == searchId,
                x => x.ApplyOrdering(columnsMap, orderBy),
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _postUnitOfWork.CommentRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Comment entity)
        {
            var updateEntity = await GetByIdAsync(entity.Id);
            if (updateEntity == null)
            {
                throw new NotFoundException("Comment Not Found", nameof(updateEntity));
            }
            updateEntity.Id = entity.Id;
            updateEntity.IsAuthorized = entity.IsAuthorized;
            await _postUnitOfWork.CommentRepository.UpdateAsync(updateEntity);
            await _postUnitOfWork.SaveChangesAsync();
        }
        public async Task<Comment> DeleteAsync(int id)
        {
            var comment = await GetByIdAsync(id);
            if (comment == null)
            {
                throw new NotFoundException("Comment Not Found", nameof(comment));
            }
            await _postUnitOfWork.CommentRepository.DeleteAsync(id);
            await _postUnitOfWork.SaveChangesAsync();
            return comment;
        }
        public void Dispose()
        {
            _postUnitOfWork?.Dispose();
        }
    }
}
