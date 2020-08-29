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
        private readonly IBlogUnitOfWork _blogUnitOfWork;
        private ICurrentUserService _currentUserService;
        public CatergoryService(IBlogUnitOfWork blogUnitOfWork, ICurrentUserService currentUserService)
        {
            _blogUnitOfWork = blogUnitOfWork;
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

            var result = await _blogUnitOfWork.CategoryRepository.GetAsync<Category>(
                x => x, x => x.Name.Contains(searchText),
                x => x.ApplyOrdering(columnsMap, orderBy), null,
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _blogUnitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Category entity)
        {
            var isExists = await _blogUnitOfWork.CategoryRepository.IsExistsAsync(x => x.Name == entity.Name && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Name));

            await _blogUnitOfWork.CategoryRepository.AddAsync(entity);
            await _blogUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            var isExists = await _blogUnitOfWork.CategoryRepository.IsExistsAsync(x => x.Name == entity.Name && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Name));

            var updateEntity = await GetByIdAsync(entity.Id);
            updateEntity.Name = entity.Name;
            await _blogUnitOfWork.CategoryRepository.UpdateAsync(updateEntity);
            await _blogUnitOfWork.SaveChangesAsync();
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            await _blogUnitOfWork.CategoryRepository.DeleteAsync(id);
            await _blogUnitOfWork.SaveChangesAsync();
            return category;
        }

        public void Dispose()
        {
            _blogUnitOfWork?.Dispose();
        }
    }
}

