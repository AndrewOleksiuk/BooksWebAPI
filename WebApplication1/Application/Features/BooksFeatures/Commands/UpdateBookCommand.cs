using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.BooksFeatures.Commands {
  public class UpdateBookCommand : IRequest<int> {
    public Book Book { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateBookCommand, int> {
      private readonly IApplicationDbContext _context;
      public UpdateProductCommandHandler(IApplicationDbContext context) {
        _context = context;
      }
      public async Task<int> Handle(UpdateBookCommand command, CancellationToken cancellationToken) {
        var book = _context.Books.Where(a => a.Id == command.Book.Id).FirstOrDefault();

        if (book == null) {
          return default;
        } else {
          book.Name = command.Book.Name;
          book.Notice = command.Book.Notice;
          book.Publisher = command.Book.Publisher;
          book.ImagePath = command.Book.ImagePath;
          await _context.SaveChangesAsync();
          return book.Id;
        }
      }
    }
  }
}
