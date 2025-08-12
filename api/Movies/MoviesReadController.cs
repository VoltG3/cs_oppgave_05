using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;
using cs_oppgave_05.Api.Movies.Contracts;
using cs_oppgave_05.Data;
using cs_oppgave_05.Data.DTOs.Movie;

namespace cs_oppgave_05.Api.Movies
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesReadController : ControllerBase, IMovieReadApi
    {
        private readonly AppDbContext _context;

        public MoviesReadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/movies
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Movie>), 200)]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
        {
            var movies = await _context.Movies
                .AsNoTracking()
                .ToListAsync();
            return Ok(movies);
        }

        // GET: /api/movies/{id}
        [HttpGet("{id}", Name = "GetMovieById")]
        [ProducesResponseType(typeof(Movie), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        // GET: /api/movies/{id}/details
        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(MovieDetailsDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(int id, [FromQuery] string? include)
        {
            var parts = (include ?? "")
                .Split(',', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries)
                .Select(p => p.ToLower())
                .ToHashSet();

            var dto = await _context.Movies
                .Where(m => m.MovId == id)
                .Select(m => new MovieDetailsDto(
                    m.MovId,
                    m.MovTitle,
                    m.MovYear,
                    m.MovTime,
                    m.MovLang,
                    m.MovDtRel,
                    m.MovRelCountry,
                    (parts.Count == 0 || parts.Contains("genres"))
                        ? m.MovieGenres!.Select(mg => new GenreDto(mg.GenId, mg.Genres!.GenTitle)).ToList()
                        : new List<GenreDto>(),
                    (parts.Count == 0 || parts.Contains("directors"))
                        ? m.MovieDirections!.Select(md => new DirectorDto(md.DirId, md.Director!.DirFname, md.Director!.DirLname)).ToList()
                        : new List<DirectorDto>(),
                    (parts.Count == 0 || parts.Contains("cast"))
                        ? m.MovieCasts!.Select(mc => new CastDto(mc.ActId, mc.Actor!.ActFname, mc.Actor!.ActLname, mc.Role)).ToList()
                        : new List<CastDto>(),
                    (parts.Count == 0 || parts.Contains("ratings"))
                        ? m.Ratings!.Select(r => new RatingDto(r.RevId, r.RevStars, r.NumOfRatings)).ToList()
                        : new List<RatingDto>()
                ))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (dto == null) return NotFound();
            return Ok(dto);
        }
    }
    
}
