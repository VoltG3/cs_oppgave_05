using cs_oppgave_05.Data.DTOs.MovieCast;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;
using cs_oppgave_05.Data.DTOs.MovieCasts;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieCastsController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public MovieCastsController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/movie_casts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieCast>>> GetAll(
            [FromQuery] int? actId,
            [FromQuery] int? movId)
        {
            var query = _context.MovieCasts
                .Include(mc => mc.Movie)
                .Include(mc => mc.Actor)
                .AsNoTracking()
                .AsQueryable();

            if (actId.HasValue && movId.HasValue)
            {
                var one = await query.FirstOrDefaultAsync(mc => mc.ActId == actId && mc.MovId == movId);
                if (one == null) return NotFound();
                return Ok(one);
            }

            var list = await query.ToListAsync();
            return Ok(list);
        }

        // GET: api/movie_casts/{actId}/{movId}
        [HttpGet("{actId:int}/{movId:int}")]
        public async Task<ActionResult<MovieCast>> GetById(int actId, int movId)
        {
            var movieCast = await _context.MovieCasts
                .Include(mc => mc.Movie)
                .Include(mc => mc.Actor)
                .AsNoTracking()
                .FirstOrDefaultAsync(mc => mc.ActId == actId && mc.MovId == movId);

            if (movieCast == null) return NotFound();
            return Ok(movieCast);
        }
        
        // POST: api/movie_casts
        [HttpPost]
        public async Task<ActionResult<MovieCast>> Create([FromBody] CreateMovieCastDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            // CHECK – exist Actor and Movie
            var actorExists = await _context.Actors.AnyAsync(a => a.ActId == dto.ActId);
            var movieExists = await _context.Movies.AnyAsync(m => m.MovId == dto.MovId);
            if (!actorExists || !movieExists)
                return BadRequest("Actor or Movie not found.");

            // Check – exist link
            var exists = await _context.MovieCasts
                .AnyAsync(mc => mc.ActId == dto.ActId && mc.MovId == dto.MovId);
            if (exists)
                return Conflict("This actor is already linked to this movie.");

            // Create new record
            var movieCast = new MovieCast
            {
                ActId = dto.ActId,
                MovId = dto.MovId,
                Role  = dto.Role
            };

            _context.MovieCasts.Add(movieCast);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { actId = movieCast.ActId, movId = movieCast.MovId }, movieCast);
        }
        
        // PATCH:
        
        [HttpPatch("{actId:int}/{movId:int}")]
        public async Task<IActionResult> Patch(int actId, int movId, [FromBody] UpdateMovieCastDto dto)
        {
            var mc = await _context.MovieCasts.FirstOrDefaultAsync(x => x.ActId == actId && x.MovId == movId);
            if (mc == null) return NotFound();

            if (dto.Role != null) mc.Role = dto.Role;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        // DELETE
        [HttpDelete("{actId:int}/{movId:int}")]
        public async Task<IActionResult> Delete(int actId, int movId)
        {
            var entity = await _context.MovieCasts.FindAsync(new object[] { actId, movId });
            if (entity == null) return NotFound();
            _context.MovieCasts.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE DTO
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] MovieCastDeleteDto dto)
        {
            var entity = await _context.MovieCasts.FindAsync(new object[] { dto.ActId, dto.MovId });
            if (entity == null) return NotFound();
            _context.MovieCasts.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}
