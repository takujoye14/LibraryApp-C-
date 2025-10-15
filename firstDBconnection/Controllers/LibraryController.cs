using Microsoft.AspNetCore.Mvc;
using firstDBconnection.Models;
using firstDBconnection.Services;
using System.Threading.Tasks;

namespace firstDBconnection.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryService _service;

        public LibraryController(LibraryService service)
        {
            _service = service;
        }

        [HttpPost("authors")]
        public async Task<IActionResult> AddAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var added = await _service.AddAuthor(author);
            return Ok(added);
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAuthors() => Ok(await _service.GetAuthors());

        [HttpPost("books")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var author = await _service.GetAuthor(book.AuthorId);
            if (author == null) return BadRequest("Author not found");
            var added = await _service.AddBook(book);
            return Ok(added);
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks() => Ok(await _service.GetBooks());
    }
}