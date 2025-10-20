using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstDBconnection.Models;
using firstDBconnection; 
using System.Linq;
using System.Threading.Tasks;
using firstDBconnection.DTO; 

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

            if (book.Author == null)
            {
                book.Author = await _context.Authors.FindAsync(book.AuthorId);
            }

            var bookDto = new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                Author = book.Author == null ? null : new AuthorDTO
                {
                    Id = book.Author.Id,
                    Name = book.Author.Name,
                    Country = book.Author.Country
                }
            };
            
            
            return CreatedAtAction(nameof(GetBook), new { id = bookDto.Id }, bookDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context.Books
                .Include(b => b.Author) 
                .Select(b => new BookDTO 
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublishedYear = b.PublishedYear,
                    Author = b.Author == null ? null : new AuthorDTO
                    {
                        Id = b.Author.Id,
                        Name = b.Author.Name,
                        Country = b.Author.Country
                    }
                })
                .ToListAsync();
            
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            // Find the book by ID and project it to a BookDTO
            var book = await _context.Books
                .Include(b => b.Author)
                .Where(b => b.Id == id) 
                .Select(b => new BookDTO 
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublishedYear = b.PublishedYear,
                    Author = b.Author == null ? null : new AuthorDTO
                    {
                        Id = b.Author.Id,
                        Name = b.Author.Name,
                        Country = b.Author.Country
                    }
                })
                .FirstOrDefaultAsync(); 

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

            if (book.Author != null)
            {
                _context.Entry(book.Author).State = EntityState.Unchanged;
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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