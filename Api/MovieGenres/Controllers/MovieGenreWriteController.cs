using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.MovieGenres.Contracts;
using cs_oppgave_05.Api.MovieGenres.Dtos;

namespace cs_oppgave_05.Api.MovieGenres.Controllers
{
    [ApiController]
    [Route("api/MovieGenres")]
    public class MovieGenresWriteController : ControllerBase, IMovieGenresWriteApi
    {
        private readonly AppDbContext _context;

        public MovieGenresWriteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/movie_genres
        [HttpPost]
        [ProducesResponseType(typeof(Entities.MovieGenres), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Entities.MovieGenres>> Create([FromBody] CreateMovieGenresDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var movieExists = await _context.Movies.AnyAsync(m => m.MovId == dto.MovId);
            var genreExists = await _context.Genres.AnyAsync(g => g.GenId == dto.GenId);
            if (!movieExists || !genreExists)
                return BadRequest("Movie or Genre not found.");

            var exists = await _context.MovieGenres.AnyAsync(mg => mg.MovId == dto.MovId && mg.GenId == dto.GenId);
            if (exists)
                return Conflict("This genre is already linked to this movie.");

            var movieGenre = new Entities.MovieGenres { MovId = dto.MovId, GenId = dto.GenId };

            _context.MovieGenres.Add(movieGenre);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetMovieGenreById", new { movId = movieGenre.MovId, genId = movieGenre.GenId }, movieGenre);
        }

        // PATCH: /api/movie_genres/{movId}/{genId}
        [HttpPatch("{movId:int}/{genId:int}")]
        [ProducesResponseType(400)]
        
        
       //public IActionResult Patch(int movId, int genId)
       // {
       //     return BadRequest("movie_genres has no updatable fields; change of keys is not allowed.");
       // }
       
       
        public Task<IActionResult> Patch(int movId, int genId)
            => Task.FromResult<IActionResult>(
                BadRequest("movie_genres has no updatable fields; change of keys is not allowed."));

        // DELETE: /api/movie_genres/{movId}/{genId}
        [HttpDelete("{movId:int}/{genId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteById(int movId, int genId)
        {
            var entity = await _context.MovieGenres.FindAsync(new object[] { movId, genId });
            if (entity == null) return NotFound();

            _context.MovieGenres.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/movie_genres  (body: { "movId": 10, "genId": 3 })
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByBody([FromBody] MovieGenresDeleteDto dto)
        {
            var entity = await _context.MovieGenres.FindAsync(new object[] { dto.MovId, dto.GenId });
            if (entity == null) return NotFound();

            _context.MovieGenres.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
