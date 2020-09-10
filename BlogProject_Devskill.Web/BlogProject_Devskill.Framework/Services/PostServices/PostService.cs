using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Framework.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Services.PostServices
{
    public class PostService : IPostService, IDisposable
    {
        public IPostUnitOfWork _postUnitOfWork { get; set; }
        public PostService(IPostUnitOfWork postUnitOfWork)
        {
            _postUnitOfWork = postUnitOfWork;
        }
        public async Task<(IList<BlogPost> Items, int Total, int TotalFilter)> GetAllPostAsync(
            string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<BlogPost, object>>>()
            {
                ["name"] = v => v.Title
            };

            var result = await _postUnitOfWork.PostRepository.GetAsync<BlogPost>(
                x => x, x =>  x.Title.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy), x => x.Include(i => i.BlogCategories).ThenInclude(i => i.Category),
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _postUnitOfWork.PostRepository.GetByIdAsync(id);
        }

        public async Task<BlogPost> GetWithIncludeByIdAsync(int id)
        {
            return await _postUnitOfWork.PostRepository.GetFirstOrDefaultAsync(x => x, x => x.Id == id, x => x.Include(i => i.BlogCategories).ThenInclude(i => i.Category));
        }

        public async Task<int> GetIdByTitleAsync(string title)
        {
            var blog = await _postUnitOfWork.PostRepository.GetFirstOrDefaultAsync(x => x, x => x.Title == title, null, true);
            return (blog == null ? -1 : blog.Id);
        }

        public async Task AddAsync(BlogPost entity)
        {
            if(entity.Title == null)
            {
                throw new NotFoundException(nameof(entity.Title),entity.Id);
            }
            await _postUnitOfWork.PostRepository.AddAsync(entity);
            await _postUnitOfWork.SaveChangesAsync();
        }

        public async Task AddBlogCategory(IList<BlogCategory> blogCategories)
        {
            await _postUnitOfWork.BlogCategoryRepository.AddRangeAsync(blogCategories);
            _postUnitOfWork.Save();
        }

        public async Task UpdateAsync(BlogPost entity)
        {
            var updateEntity = await GetByIdAsync(entity.Id);
            if (updateEntity.BlogCategories != null)
            {
                var results = _postUnitOfWork.BlogCategoryRepository.Get(x => x.BlogPostId == updateEntity.Id);
                {
                    foreach (var result in results)
                    {
                        var category = await _postUnitOfWork.CategoryRepository.GetByIdAsync(result.CategoryId);
                        category.Id = result.CategoryId;
                        category.PostCount -= 1;
                        await _postUnitOfWork.CategoryRepository.UpdateAsync(category);
                    }
                }
                await _postUnitOfWork.BlogCategoryRepository.DeleteAsync(x => x.BlogPostId == updateEntity.Id);
            }
            updateEntity.Id = entity.Id;
            updateEntity.Title = entity.Title;
            updateEntity.Description = entity.Description;
            updateEntity.CoverImageUrl = entity.CoverImageUrl;
            updateEntity.LastEditTime = DateTime.Now;
            updateEntity.UseAdminInfo = entity.UseAdminInfo;
            updateEntity.AuthorImageUrl = entity.AuthorImageUrl;
            await _postUnitOfWork.PostRepository.UpdateAsync(updateEntity);
            await _postUnitOfWork.SaveChangesAsync();
        }
        public async Task<BlogPost> DeleteAsync(int id)
        {
            var post = await GetByIdAsync(id);
            if (post == null) throw new NotFoundException(nameof(post), id);
            await _postUnitOfWork.PostRepository.DeleteAsync(id);
            if(post.BlogCategories!=null)
            {
                var results = _postUnitOfWork.BlogCategoryRepository.Get(x=>x.BlogPostId==id);
                {
                    foreach(var result in results)
                    {
                        var updateEntity = await _postUnitOfWork.CategoryRepository.GetByIdAsync(result.CategoryId);
                        updateEntity.Id = result.CategoryId;
                        updateEntity.PostCount -= 1;
                        await _postUnitOfWork.CategoryRepository.UpdateAsync(updateEntity);
                    }
                }
                await _postUnitOfWork.BlogCategoryRepository.DeleteAsync(x => x.BlogPostId == id);
            }
            await _postUnitOfWork.SaveChangesAsync();
            return post;
        }


        public void Dispose()
        {
            _postUnitOfWork?.Dispose();
        }
    }

}

