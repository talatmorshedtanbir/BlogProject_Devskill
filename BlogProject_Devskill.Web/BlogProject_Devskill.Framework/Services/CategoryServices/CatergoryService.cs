using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Framework.UnitOfWorks;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Services.CategoryServices
{
    public class CatergoryService : ICategoryService
    {
        private readonly IPostUnitOfWork _postUnitOfWork;
        private ICurrentUserService _currentUserService;
        public CatergoryService(IPostUnitOfWork postUnitOfWork, ICurrentUserService currentUserService)
        {
            _postUnitOfWork = postUnitOfWork;
            _currentUserService = currentUserService;
        }
        public async Task<(IList<Category> Items, int Total, int TotalFilter)> GetAllAsync(
         string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Category, object>>>()
            {
                ["name"] = v => v.Name,
                ["postCount"] = v => v.PostCount
            };

            var result = await _postUnitOfWork.CategoryRepository.GetAsync<Category>(
                x => x, x => x.Name.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy), null,
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public IList<Category> GetAllCategory()
        {
            var result = _postUnitOfWork.CategoryRepository.GetAll();
            return result;
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _postUnitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Category entity)
        {
            var isExists = await _postUnitOfWork.CategoryRepository.IsExistsAsync(x => x.Name == entity.Name && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Name));

            await _postUnitOfWork.CategoryRepository.AddAsync(entity);
            await _postUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            var isExists = await _postUnitOfWork.CategoryRepository.IsExistsAsync(x => x.Name == entity.Name && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Name));

            var updateEntity = await GetByIdAsync(entity.Id);
            updateEntity.Name = entity.Name;
            await _postUnitOfWork.CategoryRepository.UpdateAsync(updateEntity);
            await _postUnitOfWork.SaveChangesAsync();
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            await _postUnitOfWork.CategoryRepository.DeleteAsync(id);
            await _postUnitOfWork.SaveChangesAsync();
            return category;
        }

        public void Dispose()
        {
            _postUnitOfWork?.Dispose();
        }
    }
}

