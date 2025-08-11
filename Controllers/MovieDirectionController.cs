using cs_oppgave_05.Data.DTOs.MovieDirection;
using cs_oppgave_05.Data.DTOs.MovieDirections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

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

        // GET: api/moviedirection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDirection>>> GetAll(
            [FromQuery] int? dirId,
            [FromQuery] int? movId)
        {
            var query = _context.MovieDirections
                .Include(md => md.Movie)
                .Include(md => md.Director)
                .AsNoTracking()
                .AsQueryable();

            if (dirId.HasValue && movId.HasValue)
            {
                var one = await query
                    .FirstOrDefaultAsync(md => md.DirId == dirId && md.MovId == movId);
                if (one == null) return NotFound();
                return Ok(one);
            }

            var list = await query.ToListAsync();
            return Ok(list);
        }

        // GET: api/moviedirection/{dirId}/{movId}
        [HttpGet("{dirId:int}/{movId:int}")]
        public async Task<ActionResult<MovieDirection>> GetById(int dirId, int movId)
        {
            var movieDirection = await _context.MovieDirections
                .Include(md => md.Movie)
                .Include(md => md.Director)
                .AsNoTracking()
                .FirstOrDefaultAsync(md => md.DirId == dirId && md.MovId == movId);

            if (movieDirection == null) return NotFound();
            return Ok(movieDirection);
        }

        // POST: api/moviedirection
        [HttpPost]
        public async Task<ActionResult<MovieDirection>> Create([FromBody] CreateMovieDirectionDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var directorExists = await _context.Directors.AnyAsync(d => d.DirId == dto.DirId);
            var movieExists = await _context.Movies.AnyAsync(m => m.MovId == dto.MovId);

            if (!directorExists || !movieExists)
                return BadRequest("Director or Movie not found.");

            var exists = await _context.MovieDirections
                .AnyAsync(md => md.DirId == dto.DirId && md.MovId == dto.MovId);
            if (exists)
                return Conflict("This director is already linked to this movie.");

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

        [HttpPatch("{dirId:int}/{movId:int}")]
        public IActionResult Patch(int dirId, int movId)
        {
            return BadRequest("movie_direction has no updatable fields; change of keys is not allowed.");
        }

        [HttpDelete("{dirId:int}/{movId:int}")]
        public async Task<IActionResult> Delete(int dirId, int movId)
        {
            var entity = await _context.MovieDirections.FindAsync(dirId, movId);
            if (entity == null) return NotFound();
            _context.MovieDirections.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] MovieDirectionDeleteDto dto)
        {
            var entity = await _context.MovieDirections.FindAsync(dto.DirId, dto.MovId);
            if (entity == null) return NotFound();
            _context.MovieDirections.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
