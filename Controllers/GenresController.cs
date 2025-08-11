using cs_oppgave_05.Data.DTOs.Genres;
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
        public async Task<ActionResult<Genres>> Create([FromBody] CreateGenreDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var genre = new Genres
            {
                GenTitle = dto.GenTitle
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = genre.GenId }, genre);
        }
        
        // PATCH:
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] System.Text.Json.JsonElement changes)
        {
            var entity = await _context.Genres.FindAsync(id);
            if (entity == null) return NotFound();

            if (changes.TryGetProperty("genTitle", out var t)) entity.GenTitle = t.GetString();

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
