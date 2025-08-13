using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;
using cs_oppgave_05.Api.MovieDirections.Contracts;
using cs_oppgave_05.Data;

namespace cs_oppgave_05.Api.MovieDirections
{
    [ApiController]
    [Route("api/movie_directions")]
    public class MovieDirectionsReadController : ControllerBase, IMovieDirectionReadApi
    {
        private readonly AppDbContext _context;

        public MovieDirectionsReadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/movie_directions
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieDirection>), 200)]
        public async Task<ActionResult<IEnumerable<MovieDirection>>> GetAll([FromQuery] int? dirId, [FromQuery] int? movId)
        {
            var query = _context.MovieDirections
                .Include(md => md.Movie)
                .Include(md => md.Director)
                .AsNoTracking()
                .AsQueryable();

            if (dirId.HasValue && movId.HasValue)
            {
                var one = await query.FirstOrDefaultAsync(md => md.DirId == dirId && md.MovId == movId);
                if (one == null) return NotFound();
                return Ok(one);
            }

            var list = await query.ToListAsync();
            return Ok(list);
        }

        // GET: /api/movie_directions/{dirId}/{movId}
        [HttpGet("{dirId:int}/{movId:int}", Name = "GetMovieDirectionById")]
        [ProducesResponseType(typeof(MovieDirection), 200)]
        [ProducesResponseType(404)]
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
    }
    
}
