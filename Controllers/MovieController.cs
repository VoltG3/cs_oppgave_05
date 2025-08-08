using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
        {
            var movies = await _context.Movies.ToListAsync();
            return Ok(movies);
        }
        
        // GET: api/movies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }
        
        // POST: api/movies
        [HttpPost]
        public async Task<ActionResult<Movie>> Create([FromBody] Movie movies)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _context.Movies.Add(movies);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = movies.MovId }, movies);
        }
    }
}
