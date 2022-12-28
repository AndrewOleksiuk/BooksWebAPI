using Application.Features.BooksFeatures.Commands;
using Application.Features.BooksFeatures.Queries;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WebApi.Controllers.v1 {
  public class BooksController : BaseApiController {
    /// <summary>
    /// Creates a New Product.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookCommand command) {
      return Ok(await Mediator.Send(command));
    }
    /// <summary>
    /// Gets all Products.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll() {
      return Ok(await Mediator.Send(new GetAllBooksQuery()));
    }
    /// <summary>
    /// Gets Product Entity by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) {
      return Ok(await Mediator.Send(new GetBookByIdQuery { Id = id }));
    }
    /// <summary>
    /// Deletes Product Entity based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      return Ok(await Mediator.Send(new DeleteBookByIdCommand { Id = id }));
    }
    /// <summary>
    /// Updates the Product Entity based on Id.   
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("[action]")]
    public async Task<IActionResult> Update(int id, UpdateBookCommand command) {
      if (id != command.Book.Id) {
        return BadRequest();
      }
      return Ok(await Mediator.Send(command));
    }
  }
}