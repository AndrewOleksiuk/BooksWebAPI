using Application.Dto;
using Application.Features.BooksFeatures.Queries;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using TestProject.Setup;
using static Application.Features.BooksFeatures.Queries.GetBookByIdQuery;

namespace TestProject.Queries {
  public class GetBookByIdQueryHandlerTests {
    private readonly GetBookByIdQueryHandler _handler;
    private readonly IApplicationDbContext _mockApplicationContext;
    private SearchFiltersDto _searchFiltersDto;

    public GetBookByIdQueryHandlerTests() {
      _mockApplicationContext = Substitute.For<IApplicationDbContext>();
      _handler = new GetBookByIdQueryHandler(_mockApplicationContext);

      var books = BooksHelper.GetBooksList().AsAsyncQueryable();
      var mockSet = Substitute.For<DbSet<Book>, IQueryable<Book>>();
      ((IQueryable<Book>)mockSet).Provider.Returns(books.Provider);
      ((IQueryable<Book>)mockSet).Expression.Returns(books.Expression);
      ((IQueryable<Book>)mockSet).ElementType.Returns(books.ElementType);
      ((IQueryable<Book>)mockSet).GetEnumerator().Returns(books.GetEnumerator());

      _mockApplicationContext.Books.Returns(mockSet);
    }

    [Fact]
    public async Task GetBookById_BookExists_ReturnsBook() {
      // Arrange
      int expectedId = 3;
      string expectedName = "AAAA";

      _searchFiltersDto = new SearchFiltersDto {
        AlphabetOrder = false,
        OnlyRead = false,
        FilterText = ""
      };

      // Act
      var result = await _handler.Handle(new GetBookByIdQuery { Id = expectedId }, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(expectedId, result.Id);
      Assert.Equal(expectedName, result.Name);
    }

    [Fact]
    public async Task GetBookById_BookNotExists_ReturnsNull() {
      // Arrange
      int expectedId = 43;

      _searchFiltersDto = new SearchFiltersDto {
        AlphabetOrder = false,
        OnlyRead = false,
        FilterText = ""
      };

      // Act
      var result = await _handler.Handle(new GetBookByIdQuery { Id = expectedId }, CancellationToken.None);

      // Assert
      Assert.Null(result);
    }
  }
}
