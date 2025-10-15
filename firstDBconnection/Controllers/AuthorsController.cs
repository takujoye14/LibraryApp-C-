using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstDBconnection.Models;
using firstDBconnection; 
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace firstDBconnection.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly Library_App _context;

        public AuthorsController(Library_App context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();

            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id); // Corrected to fetch a single author by ID

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }
    }
}