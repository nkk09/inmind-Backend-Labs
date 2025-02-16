using lab1_nour_kassem.Models;
using lab1_nour_kassem.Services;
using Microsoft.AspNetCore.Mvc;

namespace lab1_nour_kassem.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("year")]
    public ActionResult<List<Author>> GetAuthorsGroupedByYear()
    {
        return Ok(_authorService.GroupAuthorsByYear());
    }

    [HttpGet("year&country")]
    public ActionResult<List<Author>> GetAuthorsGroupedByYearAndCountry()
    {
        return Ok(_authorService.GroupAuthorsByYearAndCountry());
    }
}