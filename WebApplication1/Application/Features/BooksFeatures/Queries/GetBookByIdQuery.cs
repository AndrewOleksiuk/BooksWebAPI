using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.BooksFeatures.Queries {
  public class GetBookByIdQuery : IRequest<Book> {

    public int Id { get; set; }

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book> {

      private readonly IApplicationDbContext _context;

      public GetBookByIdQueryHandler(IApplicationDbContext context) {
        _context = context;
      }

      public async Task<Book> Handle(GetBookByIdQuery query, CancellationToken cancellationToken) {
        var book = _context.Books.Where(a => a.Id == query.Id).FirstOrDefault();
        if (book == null) {
          return null;
        }
        return book;
      }
    }
  }
}
