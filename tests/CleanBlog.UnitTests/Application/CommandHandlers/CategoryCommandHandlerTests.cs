using CleanBlog.Application.Commands.Category.Handlers;
using CleanBlog.Application.Commands.Category;
using CleanBlog.Application.Specifications.Category;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Entities.Category;
using CleanBlog.Application.Exceptions;

namespace CleanBlog.UnitTests.Application.CommandHandlers
{
    public class CategoryCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Throw_ValidationException_When_Category_Already_Exists()
        {
            // Arrange
            var mockRepository = new Mock<IAsyncCommandRepository<Category, int>>();
            var mockEventBus = new Mock<IBus>();

            var handler = new CategoryCommandHandler(mockRepository.Object, mockEventBus.Object);

            var request = new CreateCategoryCommand("ExistingCategory", 0);

            // Assume category already exists
            mockRepository.Setup(repo => repo.GetAsync(It.IsAny<GetCategoryByTitleSpec>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category("ExistingCategory", null));

            // Act and Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_Should_Throw_ValidationException_When_Parent_Category_Not_Found()
        {
            // Arrange
            var mockRepository = new Mock<IAsyncCommandRepository<Category, int>>();
            var mockEventBus = new Mock<IBus>();

            var handler = new CategoryCommandHandler(mockRepository.Object, mockEventBus.Object);

            var request = new CreateCategoryCommand("NewCategory", 42);

            // Assume parent category does not exist
            mockRepository.Setup(repo => repo.GetAsync(request.ParentId.Value, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category)null);

            // Act and Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
