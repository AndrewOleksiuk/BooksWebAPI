using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BooksFeatures.Queries {
  public class GetBooksQuery : IRequest<IEnumerable<Book>> {

    public SearchFiltersDto SearchFilters { get; set; }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<Book>> {
      
      private readonly IApplicationDbContext _context;

      public GetBooksQueryHandler(IApplicationDbContext context) {
        _context = context;
      }

      public async Task<IEnumerable<Book>> Handle(GetBooksQuery query, CancellationToken cancellationToken) {
        var booksList = await _context.Books.Where(b => 
          b != null && b.Name.Contains(query.SearchFilters.FilterText) &&
          (query.SearchFilters.OnlyRead && b.IsRead == true || query.SearchFilters.OnlyRead != true)
        ).ToListAsync();

        if (query.SearchFilters.AlphabetOrder) {
          booksList.OrderBy(b => b.Name);
        }

        if (booksList == null) {
          return null;
        }
        return booksList.AsReadOnly();
      }
    }
  }
}
