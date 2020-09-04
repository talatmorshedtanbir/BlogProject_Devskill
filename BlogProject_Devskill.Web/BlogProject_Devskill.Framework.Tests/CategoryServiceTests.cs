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
        public async Task AddAsync_CategoryAlreadyExists_ThrowsDuplicationException()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name= "Test"
            };
            var categoryToMatch = new Category
            {
                Id=1,
                Name="Test"
            };
            _postUnitOfWorkMock.Setup(x => x.CategoryRepository)
                .Returns(_categoryRepositoryMock.Object);

            //await _categoryRepositoryMock.Setup( x=> x.IsExistsAsync(
            //     It.Is<Expression<Func<Category, bool>>>(y => y.Compile() (categoryToMatch))))
            //    .Returns(null).Verifiable();

            // Act
            Should.Throw<DuplicationException>(() =>
                   _categoryService.AddAsync(category));

            // Assert
            _categoryRepositoryMock.VerifyAll();
        }
    }
}