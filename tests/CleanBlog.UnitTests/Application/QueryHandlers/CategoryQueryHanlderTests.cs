using CleanBlog.Application.Queries.Category.Handlers;
using CleanBlog.Application.Queries.Category;
using CleanBlog.Application.QueryEntities.Category;
using CleanBlog.Application.Specifications.Category;
using CleanBlog.Domain.Abstractions;

namespace CleanBlog.UnitTests.Application.QueryHandlers
{
    public class CategoryQueryHanlderTests
    {
        [Fact]
        public async Task Handle_Should_Return_Categories()
        {
            // Arrange
            var mockRepository = new Mock<IAsyncQueryRepository>();
            var queryHandler = new CategoryQueryHandler(mockRepository.Object);

            var request = new GetCategoriesQuery("SampleTitle");
            var expectedCategories = new List<CategoryQueryEntity>
            {
                new CategoryQueryEntity("1", "Category1", null, null),
                new CategoryQueryEntity("2", "Category2", 1, "Category1")
            };

            mockRepository.Setup(repo => repo.GetListAsync<CategoryQueryEntity>(
                It.IsAny<SearchCategoriesByTitleSpec>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCategories);

            // Act
            var result = await queryHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCategories.Count, result.Count);

            Assert.Equal(expectedCategories[0].Id, result[0].Id);
            Assert.Equal(expectedCategories[0].Title, result[0].Title);
            Assert.Equal(expectedCategories[0].ParentTitle, result[0].ParentTitle);
        }
    }
}
