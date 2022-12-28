using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.BooksFeatures.Commands {
  public class CreateBookCommand : IRequest<int> {
    public BookPostDto Book { get; set; }
    public class CreateProductCommandHandler : IRequestHandler<CreateBookCommand, int> {
      private readonly IApplicationDbContext _context;
      public CreateProductCommandHandler(IApplicationDbContext context) {
        _context = context;
      }
      public async Task<int> Handle(CreateBookCommand command, CancellationToken cancellationToken) {
        var book = new Book() {
          Name = command.Book.Name,
          Notice = command.Book.Notice,
          IsRead = command.Book.IsRead,
          Publisher = command.Book.Publisher
        };
        
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book.Id;
      }
    }
  }
}
