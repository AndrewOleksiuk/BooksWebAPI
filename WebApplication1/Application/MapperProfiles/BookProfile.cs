using Application.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.MapperProfilers {
  public class BookProfile : Profile {
    public BookProfile() {

      CreateMap<Book, BookDto>();
      CreateMap<BookDto, Book>();
      CreateMap<Book, BookPostDto>();
      CreateMap<BookPostDto, Book>();
    }
  }
}
