using Microsoft.AspNetCore.Http;

namespace Application.Dto {
  public class BookDto {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Publisher { get; set; }
    public string? Notice { get; set; }
    public bool? IsRead { get; set; }
  }
}
