using Autofac.Extras.Moq;
using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Framework.Repositories;
using BlogProject_Devskill.Framework.Repositories.CategoryRepos;
using BlogProject_Devskill.Framework.Services.CategoryServices;
using BlogProject_Devskill.Framework.Services.PostServices;
using BlogProject_Devskill.Framework.UnitOfWorks;
using BlogProject_Devskill.Membership.Services;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Tests
{
    [ExcludeFromCodeCoverage]
    public class CategoryServiceTests
    {
        private AutoMock _mock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<IPostUnitOfWork> _postUnitOfWorkMock;
        private ICategoryService _categoryService;
        private Mock<ICurrentUserService> _currentUserServiceMock;

        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanUp()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            _categoryRepositoryMock = _mock.Mock<ICategoryRepository>();
            _postUnitOfWorkMock = _mock.Mock<IPostUnitOfWork>();
            _categoryService = _mock.Create<CatergoryService>();
            _currentUserServiceMock = _mock.Mock<ICurrentUserService>();
        }
        [TearDown]
        public void Clean()
        {
            _categoryRepositoryMock.Reset();
            _postUnitOfWorkMock.Reset();
        }

        [Test]
        public void AddAsync_CategoryAlreadyExists_ThrowsDuplicationException()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name= "Test"
            };
            var categoryToMatch = new Category
            {
                Id = 2,
                Name="Test"
            };

            _postUnitOfWorkMock.Setup(x => x.CategoryRepository)
                .Returns(_categoryRepositoryMock.Object);

              _categoryRepositoryMock.Setup(x => x.IsExistsAsync(
                It.Is<Expression<Func<Category, bool>>>(y => y.Compile()(categoryToMatch))))
                .ReturnsAsync(true).Verifiable();

            // Act
            Should.Throw<DuplicationException>(() =>
                    _categoryService.AddAsync(category));

            // Assert
            _categoryRepositoryMock.VerifyAll();
        }

        [Test]
        public void AddAsync_CategoryDoesNotExists_SavesCategory()
        {
            //Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Test"
            };
            var categoryToMatch = new Category
            {
                Id = 2,
                Name = "Test"
            };

            _postUnitOfWorkMock.Setup(x => x.CategoryRepository)
            .Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup(x => x.IsExistsAsync(
              It.Is<Expression<Func<Category, bool>>>(y => y.Compile()(categoryToMatch))))
              .ReturnsAsync(false).Verifiable();

            _categoryRepositoryMock.Setup(x => x.AddAsync(category)).Returns(Task.CompletedTask).Verifiable();
            _postUnitOfWorkMock.Setup( x => x.SaveChangesAsync()).Verifiable();

            // Act
             _categoryService.AddAsync(category);

            // Assert
            _categoryRepositoryMock.VerifyAll();
            _postUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public void UpdateAsync_CategoryAlreadyExists_ThrowsDuplicationException()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Test"
            };
            var categoryToMatch = new Category
            {
                Id = 2,
                Name = "Test"
            };

            _postUnitOfWorkMock.Setup(x => x.CategoryRepository)
                .Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup(x => x.IsExistsAsync(
              It.Is<Expression<Func<Category, bool>>>(y => y.Compile()(categoryToMatch))))
              .ReturnsAsync(true).Verifiable();

            // Act
            Should.Throw<DuplicationException>( () =>
                    _categoryService.UpdateAsync(category));

            // Assert
            _categoryRepositoryMock.VerifyAll();
        }

        [Test]
        public void DeleteAsync_CategoryDoesNotExists_ThrowsNotFoundException()
        {
            // Arrange
            var category = new Category
            {
                Id = 2,
                Name = "Test"
            };
            var categoryReturned = new Category();
            categoryReturned = null;

            _postUnitOfWorkMock.Setup(x => x.CategoryRepository)
                .Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup( x =>  x.GetByIdAsync(category.Id))
                .ReturnsAsync(categoryReturned).Verifiable();

            // Act
            Should.Throw<NotFoundException>( () =>
                    _categoryService.DeleteAsync(category.Id));

            // Assert
            _categoryRepositoryMock.VerifyAll();
        }

        [Test]
        public void DeleteAsync_CategoryAlreadyExists_DeletesCategory()
        {
            // Arrange
            var category = new Category
            {
                Id = 2,
                Name = "Test"
            };
            var categoryReturned = new Category();
            categoryReturned = null;

            _postUnitOfWorkMock.Setup(x => x.CategoryRepository)
                .Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock.Setup(x => x.GetByIdAsync(category.Id))
                .ReturnsAsync(category).Verifiable();

            _categoryRepositoryMock.Setup(x => x.DeleteAsync(category.Id)).Returns(Task.CompletedTask).Verifiable();

            _postUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Verifiable();

            // Act
            _categoryService.DeleteAsync(category.Id);

            // Assert
            _categoryRepositoryMock.VerifyAll();
            _postUnitOfWorkMock.VerifyAll();
        }
    }
}