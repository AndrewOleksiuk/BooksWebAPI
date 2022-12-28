using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context {
  public class BooksContext : DbContext, IApplicationDbContext {
    public BooksContext(DbContextOptions<BooksContext> options) : base(options) {
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfiguration(new BookConfiguration());
    }

    protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseInMemoryDatabase(databaseName: "BooksDb");
    }

    public async Task<int> SaveChangesAsync() {
      return await base.SaveChangesAsync();
    }
  }
}
