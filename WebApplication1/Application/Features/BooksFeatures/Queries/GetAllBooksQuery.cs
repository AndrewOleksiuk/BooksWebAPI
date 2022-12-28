using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BooksFeatures.Queries {
  public class GetAllBooksQuery : IRequest<IEnumerable<Book>> {

    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>> {

      private readonly IApplicationDbContext _context;

      public GetAllBooksQueryHandler(IApplicationDbContext context) {
        _context = context;
      }

      public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken) {
        var booksList = await _context.Books.ToListAsync();
        if (booksList == null) {
          return null;
        }
        return booksList.AsReadOnly();
      }
    }
  }
}
