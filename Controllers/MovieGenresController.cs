using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;
using cs_oppgave_05.Data.DTOs.MovieGenres;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieGenresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieGenresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/moviegenres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieGenres>>> GetAll()
        {
            var movieGenres = await _context.MovieGenres.ToListAsync();
            return Ok(movieGenres);
        }
        
        // GET: api/movie_genres/{id}/{id}
        [HttpGet("{movId}/{genId}")]
        public async Task<ActionResult<MovieGenres>> GetById(int movId, int genId)
        {
            var movieGenres = await _context.MovieGenres
                .FirstOrDefaultAsync(mg => mg.MovId == movId && mg.GenId == genId);

            if (movieGenres == null)
            {
                return NotFound();
            }

            return Ok(movieGenres);
        }
        
        // POST: api/movie_genres
        [HttpPost]
        public async Task<ActionResult<MovieGenres>> Create([FromBody] CreateMovieGenresDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            // CHECK – exist Movie and Genre
            var movieExists = await _context.Movies.AnyAsync(m => m.MovId == dto.MovId);
            var genreExists = await _context.Genres.AnyAsync(g => g.GenId == dto.GenId);
            if (!movieExists || !genreExists)
                return BadRequest("Movie or Genre not found.");

            // Check – exist link
            var exists = await _context.MovieGenres
                .AnyAsync(mg => mg.MovId == dto.MovId && mg.GenId == dto.GenId);
            if (exists)
                return Conflict("This genre is already linked to this movie.");

            // Create new record
            var movieGenre = new MovieGenres
            {
                MovId = dto.MovId,
                GenId = dto.GenId
            };

            _context.MovieGenres.Add(movieGenre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { movId = movieGenre.MovId, genId = movieGenre.GenId },
                movieGenre);
        }
        
        // PATCH:
        [HttpPatch("{movId:int}/{genId:int}")]
        public IActionResult Patch(int movId, int genId)
        {
            return BadRequest("movie_genres has no updatable fields; change of keys is not allowed.");
        }

    }
}
