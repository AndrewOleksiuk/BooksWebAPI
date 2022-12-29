using Domain.Entities;

namespace TestProject.Setup {
  public static class BooksHelper {
     public static IEnumerable<Book> GetBooksList() {
      return new List<Book>
        {
            new Book {Id = 1, Name = "Book 1", Publisher = "Publisher1", IsRead = false, Notice = "Will read" },
            new Book {Id = 2, Name = "Book 2", Publisher = "Publisher2", IsRead = true, Notice = "" },
            new Book {Id = 3, Name = "AAAA", Publisher = "Publisher3", IsRead = true, Notice = "123" }
        };
    }
  }
}
