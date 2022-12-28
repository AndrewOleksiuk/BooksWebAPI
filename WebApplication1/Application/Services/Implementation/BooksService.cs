using Application.Dto;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementation {
  public class BooksService : IBookService {

    private readonly IRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public BooksService(IRepository<Book> bookRepository, IMapper mapper) {
      _bookRepository = bookRepository;
      _mapper = mapper;

    }

    public async Task<BookDto> GetByIdAsync(int bookId) {
      var result = _mapper.Map<BookDto>(await _bookRepository.GetAll()
                                          .FirstOrDefaultAsync(p => p.Id == bookId));

      return result;
    }

    public async Task<BookDto> AddAsync(BookDto bookDto) {
      var book = _mapper.Map<Book>(bookDto);
    
      _bookRepository.Add(book);
      await _bookRepository.SaveChangesAsync();
      return _mapper.Map<BookDto>(book);
    }

    public Task<IEnumerable<BookDto>> GetAllAsync(string parameters) {
      throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(int bookId) {
      throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(BookDto book) {
      throw new NotImplementedException();
    }
  }
}
