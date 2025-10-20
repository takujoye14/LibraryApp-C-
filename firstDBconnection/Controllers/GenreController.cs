using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstDBconnection.Models;
using firstDBconnection;
using System.Threading.Tasks;

namespace firstDBconnection.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly Library_App _context;

        public GenresController(Library_App context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            return Ok(await _context.Genres.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }
    }
}