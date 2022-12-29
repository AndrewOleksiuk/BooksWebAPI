using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.BooksFeatures.Commands {
  public class UpdateBookCommand : IRequest<int> {
    public Book Book { get; set; }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, int> {

      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public UpdateBookCommandHandler(IApplicationDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
      }
      public async Task<int> Handle(UpdateBookCommand command, CancellationToken cancellationToken) {
        var book = _context.Books.Where(a => a.Id == command.Book.Id).FirstOrDefault();
        var newBook = _mapper.Map<Book>(command.Book);

        if (book == null) {
          return default;
        } else {
          book.Name = newBook.Name;
          book.Notice = newBook.Notice;
          book.Publisher = newBook.Publisher;
          book.IsRead = newBook.IsRead;
          _context.Books.Update(book);
          await _context.SaveChangesAsync();
          return book.Id;
        }
      }
    }
  }
}
