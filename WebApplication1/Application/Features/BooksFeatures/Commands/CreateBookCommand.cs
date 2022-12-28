using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.BooksFeatures.Commands {
  public class CreateBookCommand : IRequest<int> {
    public Book Book { get; set; }
    public class CreateProductCommandHandler : IRequestHandler<CreateBookCommand, int> {
      private readonly IApplicationDbContext _context;
      public CreateProductCommandHandler(IApplicationDbContext context) {
        _context = context;
      }
      public async Task<int> Handle(CreateBookCommand command, CancellationToken cancellationToken) {
        var book = new Book() {
          Name = command.Book.Name,
          Notice = command.Book.Notice,
          ImagePath = command.Book.ImagePath,
          Publisher = command.Book.Publisher
        };
        
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book.Id;
      }
    }
  }
}
