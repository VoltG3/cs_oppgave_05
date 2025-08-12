using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieGenres.Contracts;
using cs_oppgave_05.Data;

namespace cs_oppgave_05.Api.MovieGenres
{
    [ApiController]
    [Route("api/movie_genres")]
    public class MovieGenresReadController : ControllerBase, IMovieGenresReadApi
    {
        private readonly AppDbContext _context;

        public MovieGenresReadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/movie_genres
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Models.MovieGenres>), 200)]
        public async Task<ActionResult<IEnumerable<Models.MovieGenres>>> GetAll([FromQuery] int? movId, [FromQuery] int? genId)
        {
            var query = _context.MovieGenres
                .Include(mg => mg.Movie)
                .Include(mg => mg.Genres)
                .AsNoTracking()
                .AsQueryable();

            if (movId.HasValue && genId.HasValue)
            {
                var one = await query.FirstOrDefaultAsync(mg => mg.MovId == movId && mg.GenId == genId);
                if (one == null) return NotFound();
                return Ok(one);
            }

            var list = await query.ToListAsync();
            return Ok(list);
        }

        // GET: /api/movie_genres/{movId}/{genId}
        [HttpGet("{movId:int}/{genId:int}", Name = "GetMovieGenreById")]
        [ProducesResponseType(typeof(Models.MovieGenres), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Models.MovieGenres>> GetById(int movId, int genId)
        {
            var movieGenre = await _context.MovieGenres
                .Include(mg => mg.Movie)
                .Include(mg => mg.Genres)
                .AsNoTracking()
                .FirstOrDefaultAsync(mg => mg.MovId == movId && mg.GenId == genId);

            if (movieGenre == null) return NotFound();
            return Ok(movieGenre);
        }
    }
    
}
