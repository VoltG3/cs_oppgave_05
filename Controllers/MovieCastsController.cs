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
        public async Task<ActionResult<IEnumerable<MovieCast>>> GetAll()
        {
            var movieCasts = await _context.MovieCasts.ToListAsync();
            return Ok(movieCasts);
        }
        
        // GET: api/movie_casts/{id}/{id}
        [HttpGet("{actId}/{movId}")]
        public async Task<ActionResult<MovieCast>> GetById(int actId, int movId)
        {
            var movieCast = await _context.MovieCasts
                .FirstOrDefaultAsync(mk => mk.ActId == actId && mk.MovId == movId);

            if (movieCast == null)
            {
                return NotFound();
            }

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
    }
}