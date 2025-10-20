using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstDBconnection.Models;
using firstDBconnection;
using System.Threading.Tasks;

namespace firstDBconnection.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly Library_App _context;

        public PublishersController(Library_App context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            return Ok(await _context.Publishers.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPublisher), new { id = publisher.Id }, publisher);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }
    }
}