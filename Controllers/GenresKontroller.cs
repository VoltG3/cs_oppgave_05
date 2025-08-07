using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public GenresController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genres>>> GetAll()
        {
            var genres = await _context.Genres.ToListAsync();
            return Ok(genres);
        }
        
        // GET: api/genres/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Genres>> GetById(int id)
        {
            var genres = await _context.Genres.FindAsync(id);

            if (genres == null)
            {
                return NotFound();
            }

            return Ok(genres);
        }
    }
}