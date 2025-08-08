using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;
using cs_oppgave_05.Data.DTOs.MovieDirections;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieDirectionController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public MovieDirectionController(AppDbContext context)
        {
            _context = context;
        }
        
        // Get: api/movie_directions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDirection>>> GetAll()
        {
            var result = await _context.MovieDirections
                .Include(md => md.Movie)
                .Include(md => md.Director)
                .ToListAsync();

            return Ok(result);
        }
        
        // GET: api/movie_dierections/{id}/{id}
        [HttpGet("{dirId}/{movId}")]
        public async Task<ActionResult<MovieDirection>> GetById(int dirId, int movId)
        {
            var movieDirection = await _context.MovieDirections
                .FirstOrDefaultAsync(md => md.DirId == dirId && md.MovId == movId);

            if (movieDirection == null)
            {
                return NotFound();
            }

            return Ok(movieDirection);
        }
        
        // POST: api/movie_directions
        [HttpPost]
        public async Task<ActionResult<MovieDirection>> Create([FromBody] CreateMovieDirectionDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            // CHECK – exist Director and Movie
            var directorExists = await _context.Directors.AnyAsync(d => d.DirId == dto.DirId);
            var movieExists = await _context.Movies.AnyAsync(m => m.MovId == dto.MovId);
            if (!directorExists || !movieExists)
                return BadRequest("Director or Movie not found.");

            // Check – exist link
            var exists = await _context.MovieDirections
                .AnyAsync(md => md.DirId == dto.DirId && md.MovId == dto.MovId);
            if (exists)
                return Conflict("This director is already linked to this movie.");

            // Create new record
            var movieDirection = new MovieDirection
            {
                DirId = dto.DirId,
                MovId = dto.MovId
            };

            _context.MovieDirections.Add(movieDirection);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { dirId = movieDirection.DirId, movId = movieDirection.MovId },
                movieDirection);
        }

    }
}