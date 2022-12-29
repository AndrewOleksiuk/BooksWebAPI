using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.BooksFeatures.Commands {
  public class CreateBookCommand : IRequest<int> {
    public BookPostDto Book { get; set; }
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int> {

      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public CreateBookCommandHandler(IApplicationDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
      }
      public async Task<int> Handle(CreateBookCommand command, CancellationToken cancellationToken) {
        var book = _mapper.Map<Book>(command.Book);
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return book.Id;
      }
    }
  }
}
