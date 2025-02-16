using lab1_nour_kassem.Models;
using lab1_nour_kassem.Services;
using Microsoft.AspNetCore.Mvc;

namespace lab1_nour_kassem.Controllers;

[Route("api/books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("year")]
    public ActionResult<List<Author>> GetBooksByYear(int year, bool descending = false)
    {
        return Ok(_bookService.GetBooksByYear(year, descending));
    }

    [HttpGet("count")]
    public ActionResult<List<Author>> GetBooksCount()
    {
        return Ok(_bookService.GetBooksCount());
    }

    [HttpGet("paginated")]
    public ActionResult<List<Author>> GetBooksPaginated(int pageSize, int pageNumber)
    {
        return Ok(_bookService.GetBooksPaginated(pageSize, pageNumber));
    }
}