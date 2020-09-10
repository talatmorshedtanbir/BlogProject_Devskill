using Autofac.Extras.Moq;
using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Framework.Repositories;
using BlogProject_Devskill.Framework.Services.PostServices;
using BlogProject_Devskill.Framework.UnitOfWorks;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Framework.Tests
{
    [ExcludeFromCodeCoverage]
    public class PostServiceTests
    {
        private AutoMock _mock;
        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IPostUnitOfWork> _postUnitOfWorkMock;
        private IPostService _postService;

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
            _postRepositoryMock = _mock.Mock<IPostRepository>();
            _postUnitOfWorkMock = _mock.Mock<IPostUnitOfWork>();
            _postService = _mock.Create<PostService>();
        }
        [TearDown]
        public void Clean()
        {
            _postRepositoryMock.Reset();
            _postUnitOfWorkMock.Reset();
        }

        [Test]
        public void AddAsync_TitleIsNull_ThrowsNotFoundException()
        {
            // Arrange
            var post = new BlogPost
            {
                Id = 1,
                Description="This is a test blog"
            };

            _postUnitOfWorkMock.Setup(x => x.PostRepository)
                .Returns(_postRepositoryMock.Object);

            // Act
            Should.Throw<NotFoundException>(() =>
                    _postService.AddAsync(post));

            // Assert
            _postRepositoryMock.VerifyAll();
        }

        [Test]
        public void AddAsync_TitleIsNotNull_SavesPost()
        {
            //Arrange
            var post = new BlogPost
            {
                Id = 1,
                Title = "Test",
                Description= "This is a test post"
            };

            _postUnitOfWorkMock.Setup(x => x.PostRepository)
            .Returns(_postRepositoryMock.Object);

            _postRepositoryMock.Setup(x => x.AddAsync(post)).Returns(Task.CompletedTask).Verifiable();
            _postUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Verifiable();

            // Act
            _postService.AddAsync(post);

            // Assert
            _postRepositoryMock.VerifyAll();
            _postUnitOfWorkMock.VerifyAll();
        }

    }
}
