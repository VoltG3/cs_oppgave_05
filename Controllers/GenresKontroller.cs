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
        
        // POST: api/genres
        [HttpPost]
        public async Task<ActionResult<Director>> Create([FromBody] Genres genres)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _context.Genres.Add(genres);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = genres.GenId }, genres);
        }
    }
}
