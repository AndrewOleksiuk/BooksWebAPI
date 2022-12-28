using Domain.Common;

namespace Domain.Entities {
  public class Book : IEntityBase {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Publisher { get; set; }
    public string? Notice { get; set; }
    public string? ImagePath { get; set; }
  }
}
