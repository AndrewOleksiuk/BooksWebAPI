using System.Xml.Linq;
using Application.Dto;
using Application.Features.BooksFeatures.Queries;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using TestProject.Setup;
using static Application.Features.BooksFeatures.Queries.GetBooksQuery;

namespace TestProject.Queries
{
    public class GetBooksQueryHandlerTests {
    private readonly GetBooksQueryHandler _handler;
    private readonly IApplicationDbContext _mockApplicationContext;
    private SearchFiltersDto _searchFiltersDto;

    public GetBooksQueryHandlerTests() {
      _mockApplicationContext = Substitute.For<IApplicationDbContext>();
      _handler = new GetBooksQueryHandler(_mockApplicationContext);

      var books = BooksHelper.GetBooksList().AsAsyncQueryable();
      var mockSet = Substitute.For<DbSet<Book>, IQueryable<Book>>();
      ((IQueryable<Book>)mockSet).Provider.Returns(books.Provider);
      ((IQueryable<Book>)mockSet).Expression.Returns(books.Expression);
      ((IQueryable<Book>)mockSet).ElementType.Returns(books.ElementType);
      ((IQueryable<Book>)mockSet).GetEnumerator().Returns(books.GetEnumerator());

      _mockApplicationContext.Books.Returns(mockSet);
    }

    [Fact]
    public async Task GetAllBooks_FiltersEmpty_ReturnsAllList() {
      // Arrange
      int expectedCount = 3;

      _searchFiltersDto = new SearchFiltersDto {
        AlphabetOrder = false,
        OnlyRead = false,
        FilterText = ""
      };

      // Act
      var result = await _handler.Handle(new GetBooksQuery { SearchFilters = _searchFiltersDto }, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(expectedCount, result.Count());
    }

    [Fact]
    public async Task GetAllBooks_FiltersName_ReturnsFilteredResult() {
      // Arrange
      int expectedCount = 1;

      _searchFiltersDto = new SearchFiltersDto {
        AlphabetOrder = false,
        OnlyRead = false,
        FilterText = "Book 1"
      };

      // Act
      var result = await _handler.Handle(new GetBooksQuery { SearchFilters = _searchFiltersDto }, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(expectedCount, result.Count());
      Assert.Equal(_searchFiltersDto.FilterText, result.First().Name);
    }

    [Fact]
    public async Task GetAllBooks_AlphabetOrder_ReturnsOrderedResult() {
      // Arrange
      int expectedCount = 3;
      string expectedName = "AAAA";

      _searchFiltersDto = new SearchFiltersDto {
        AlphabetOrder = true,
        OnlyRead = false,
        FilterText = ""
      };

      // Act
      var result = await _handler.Handle(new GetBooksQuery { SearchFilters = _searchFiltersDto }, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(expectedCount, result.Count());
      Assert.Equal(expectedName, result.First().Name);
    }

    [Fact]
    public async Task GetAllBooks_OnlyRead_ReturnsFilteredResult() {
      // Arrange
      int expectedCount = 2;

      _searchFiltersDto = new SearchFiltersDto {
        AlphabetOrder = false,
        OnlyRead = true,
        FilterText = ""
      };

      // Act
      var result = await _handler.Handle(new GetBooksQuery { SearchFilters = _searchFiltersDto }, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(expectedCount, result.Count());
      Assert.DoesNotContain(result, b => b.IsRead == false);
    }

    [Fact]
    public async Task GetAllBooks_NoMAtchForFilters_ReturnsEmptyResult() {
      // Arrange
      int expectedCount = 2;

      _searchFiltersDto = new SearchFiltersDto {
        AlphabetOrder = true,
        OnlyRead = true,
        FilterText = "x"
      };

      // Act
      var result = await _handler.Handle(new GetBooksQuery { SearchFilters = _searchFiltersDto }, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Empty(result);
    }
  }
}
