using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces {
  public interface IApplicationDbContext {
    DbSet<Book> Books { get; set; }
    Task<int> SaveChangesAsync();
  }
}
