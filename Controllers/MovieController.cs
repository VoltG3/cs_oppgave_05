using cs_oppgave_05.Data.DTOs.Movie;
using cs_oppgave_05.Data.DTOs.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;
using DeleteByIdDto = cs_oppgave_05.Data.DTOs.Actor.DeleteByIdDto;

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
        public async Task<ActionResult<Movie>> Create([FromBody] CreateMovieDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var movie = new Movie
            {
                MovTitle = dto.MovTitle,
                MovYear = dto.MovYear,
                MovTime = dto.MovTime,
                MovLang = dto.MovLang,
                MovDtRel = dto.MovDtRel,
                MovRelCountry = dto.MovRelCountry
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = movie.MovId }, movie);
        }
        
        // PATCH :
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] System.Text.Json.JsonElement changes)
        {
            var entity = await _context.Movies.FindAsync(id);
            if (entity == null) return NotFound();

            if (changes.TryGetProperty("movTitle", out var title)) entity.MovTitle = title.GetString();
            if (changes.TryGetProperty("movYear", out var year) && year.ValueKind != System.Text.Json.JsonValueKind.Null) entity.MovYear = year.GetInt32();
            if (changes.TryGetProperty("movTime", out var time) && time.ValueKind != System.Text.Json.JsonValueKind.Null) entity.MovTime = time.GetInt32();
            if (changes.TryGetProperty("movLang", out var lang)) entity.MovLang = lang.GetString();
            if (changes.TryGetProperty("movDtRel", out var dtrel) && dtrel.ValueKind == System.Text.Json.JsonValueKind.String && DateTime.TryParse(dtrel.GetString(), out var dt)) entity.MovDtRel = dt;
            if (changes.TryGetProperty("movRelCountry", out var rc)) entity.MovRelCountry = rc.GetString();

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Movies.FindAsync(id);
            if (entity == null) return NotFound();
            _context.Movies.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        // DELETE DTO
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteByIdDto dto)
        {
            var entity = await _context.Movies.FindAsync(dto.Id);
            if (entity == null) return NotFound();
            _context.Movies.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        // READ only all TAble
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(int id, [FromQuery] string? include)
        {
            var parts = (include ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
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

                    // GENRES: ar ID + Title (no movie_genres)
                    (parts.Count == 0 || parts.Contains("genres"))
                        ? m.MovieGenres!
                            .Select(mg => new GenreDto(mg.GenId, mg.Genres!.GenTitle))
                            .ToList()
                        : new List<GenreDto>(),

                    // DIRECTORS: DirId + vārds/uzvārds (no movie_direction)
                    (parts.Count == 0 || parts.Contains("directors"))
                        ? m.MovieDirections!
                            .Select(md => new DirectorDto(
                                md.DirId,
                                md.Director!.DirFname,
                                md.Director!.DirLname))
                            .ToList()
                        : new List<DirectorDto>(),

                    // CAST: ActId + vārds/uzvārds + loma (NO movie_cast)
                    (parts.Count == 0 || parts.Contains("cast"))
                        ? m.MovieCasts!
                            .Select(mc => new CastDto(
                                mc.ActId,
                                mc.Actor!.ActFname,
                                mc.Actor!.ActLname,
                                mc.Role))                     
                            .ToList()
                        : new List<CastDto>(),

                    // RATINGS: RevId + zvaigznes + skaits
                    (parts.Count == 0 || parts.Contains("ratings"))
                        ? m.Ratings!
                            .Select(r => new RatingDto(
                                r.RevId,
                                r.RevStars,
                                r.NumOfRatings))
                            .ToList()
                        : new List<RatingDto>()
                ))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (dto == null) return NotFound();
            return Ok(dto);
        }
        
    }
}
