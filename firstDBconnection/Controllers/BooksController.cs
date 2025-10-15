using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstDBconnection.Models;
using firstDBconnection; 
using System.Linq;
using System.Threading.Tasks;

namespace firstDBconnection.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly Library_App _context;

        public BooksController(Library_App context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context.Books.Include(b => b.Author).ToListAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("ID in the URL doesn't match the body.");
            }

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}