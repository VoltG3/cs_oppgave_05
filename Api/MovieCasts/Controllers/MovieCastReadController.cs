using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.MovieCasts.Contracts;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.MovieCasts
{
    [ApiController]
    [Route("api/movie_casts")]
    public class MovieCastsReadController : ControllerBase, IMovieCastReadApi
    {
        private readonly AppDbContext _context;

        public MovieCastsReadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/movie_casts
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieCast>), 200)]
        public async Task<ActionResult<IEnumerable<MovieCast>>> GetAll([FromQuery] int? actId, [FromQuery] int? movId)
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

        // GET: /api/movie_casts/{actId}/{movId}
        [HttpGet("{actId:int}/{movId:int}", Name = "GetMovieCastById")]
        [ProducesResponseType(typeof(MovieCast), 200)]
        [ProducesResponseType(404)]
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
    }
    
}
