using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BooksFeatures.Commands {
  public class DeleteBookByIdCommand : IRequest<int> {
    public int Id { get; set; }
    public class DeleteBookByIdCommandHandler : IRequestHandler<DeleteBookByIdCommand, int> {
      private readonly IApplicationDbContext _context;
      public DeleteBookByIdCommandHandler(IApplicationDbContext context) {
        _context = context;
      }
      public async Task<int> Handle(DeleteBookByIdCommand command, CancellationToken cancellationToken) {
        var product = await _context.Books.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
        if (product == null) return default;
        _context.Books.Remove(product);
        await _context.SaveChangesAsync();
        return product.Id;
      }
    }
  }
}
