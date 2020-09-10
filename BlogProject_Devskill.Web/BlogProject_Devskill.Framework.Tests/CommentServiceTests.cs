using Autofac.Extras.Moq;
using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Exceptions;
using BlogProject_Devskill.Framework.Repositories.BlogRepos;
using BlogProject_Devskill.Framework.Services.CommentService;
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
    public class CommentServiceTests
    {
        private AutoMock _mock;
        private Mock<ICommentRepository> _commentRepositoryMock;
        private Mock<IPostUnitOfWork> _postUnitOfWorkMock;
        private ICommentService _commentService;

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
            _commentRepositoryMock = _mock.Mock<ICommentRepository>();
            _postUnitOfWorkMock = _mock.Mock<IPostUnitOfWork>();
            _commentService = _mock.Create<CommentService>();
        }
        [TearDown]
        public void Clean()
        {
            _commentRepositoryMock.Reset();
            _postUnitOfWorkMock.Reset();
        }

        [Test]
        public void UpdateAsync_CommentDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var comment = new Comment
            {
                Id = 1,
                Name = "Test"
            };
            var nullComment = new Comment();
            nullComment = null;

            _postUnitOfWorkMock.Setup(x => x.CommentRepository)
                .Returns(_commentRepositoryMock.Object);

            _commentRepositoryMock.Setup(x => x.GetByIdAsync(comment.Id))
              .ReturnsAsync(nullComment).Verifiable();

            // Act
            Should.Throw<NotFoundException >(() =>
                   _commentService.UpdateAsync(comment));

            // Assert
            _commentRepositoryMock.VerifyAll();
        }

        [Test]
        public void UpdateAsync_CommentDoesExist_UpdateComment()
        {
            // Arrange
            var comment = new Comment
            {
                Id = 1,
                IsAuthorized = false
            };
            var updatedComment = new Comment
            {
                IsAuthorized = true
            };
            var nullComment = new Comment();
            nullComment = null;

            _postUnitOfWorkMock.Setup(x => x.CommentRepository)
                .Returns(_commentRepositoryMock.Object);

            _commentRepositoryMock.Setup(x => x.GetByIdAsync(comment.Id))
              .ReturnsAsync(comment).Verifiable();

            comment.IsAuthorized = updatedComment.IsAuthorized;

            _commentRepositoryMock.Setup(x => x.UpdateAsync(comment)).Returns(Task.CompletedTask).Verifiable();

            _postUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Verifiable();

            // Act
            _commentService.UpdateAsync(comment);

            // Assert
            _commentRepositoryMock.VerifyAll();
        }


        [Test]
        public void DeleteAsync_CommentDoesNotExists_ThrowsNotFoundException()
        {
            // Arrange
            var comment = new Comment
            {
                Id = 1,
                Name = "Test"
            };
            var commentReturned = new Comment();
            commentReturned = null;

            _postUnitOfWorkMock.Setup(x => x.CommentRepository)
                .Returns(_commentRepositoryMock.Object);

            _commentRepositoryMock.Setup(x => x.GetByIdAsync(comment.Id))
                .ReturnsAsync(commentReturned).Verifiable();

            // Act
            Should.Throw<NotFoundException>(() =>
                   _commentService.DeleteAsync(comment.Id));

            // Assert
            _commentRepositoryMock.VerifyAll();
        }

        [Test]
        public void DeleteAsync_CommentAlreadyExists_DeletesComment()
        {
            // Arrange
            var comment = new Comment
            {
                Id = 1,
                Name = "Test"
            };

            _postUnitOfWorkMock.Setup(x => x.CommentRepository)
                .Returns(_commentRepositoryMock.Object);

            _commentRepositoryMock.Setup(x => x.GetByIdAsync(comment.Id))
                .ReturnsAsync(comment).Verifiable();

            _commentRepositoryMock.Setup(x => x.DeleteAsync(comment.Id)).Returns(Task.CompletedTask).Verifiable();

            _postUnitOfWorkMock.Setup(x => x.SaveChangesAsync()).Verifiable();

            // Act
            _commentService.DeleteAsync(comment.Id);

            // Assert
            _commentRepositoryMock.VerifyAll();
            _postUnitOfWorkMock.VerifyAll();
        }

    }
}
