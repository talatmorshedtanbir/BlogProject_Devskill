﻿using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Framework.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Services.PostServices
{
    class PostService : IPostService, IDisposable
    {
        public IPostUnitOfWork _postUnitOfWork { get; set; }
        public PostService(IPostUnitOfWork postUnitOfWork)
        {
            _postUnitOfWork = postUnitOfWork;
        }
        public async Task<(IList<BlogPost> Items, int Total, int TotalFilter)> GetAllAsync(
            string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<BlogPost, object>>>()
            {
                ["name"] = v => v.Title
            };

            var result = await _postUnitOfWork.PostRepository.GetAsync<BlogPost>(
                x => x, x => x.Title.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy),
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _postUnitOfWork.PostRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(BlogPost entity)
        {
            var isExists = await _postUnitOfWork.PostRepository.IsExistsAsync(x => x.Title == entity.Title && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Title));

            await _postUnitOfWork.PostRepository.AddAsync(entity);
            await _postUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogPost entity)
        {
            var isExists = await _postUnitOfWork.PostRepository.IsExistsAsync(x => x.Title == entity.Title && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Title));

            var updateEntity = await GetByIdAsync(entity.Id);
            updateEntity.Title = entity.Title;

            await _postUnitOfWork.PostRepository.UpdateAsync(updateEntity);
            await _postUnitOfWork.SaveChangesAsync();
        }

        public async Task<BlogPost> DeleteAsync(int id)
        {
            var post = await GetByIdAsync(id);
            await _postUnitOfWork.PostRepository.DeleteAsync(id);
            await _postUnitOfWork.SaveChangesAsync();
            return post;
        }


        public void Dispose()
        {
            _postUnitOfWork?.Dispose();
        }
    }

}

