using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;
using cs_oppgave_05.Api.MovieDirections.Contracts;
using cs_oppgave_05.Api.MovieDirections.Dtos;
using cs_oppgave_05.Api.MovieDirections.Dtos.Contracts;
using cs_oppgave_05.Infrastructure.Presistance;

namespace cs_oppgave_05.Api.MovieDirections.Controllers
{
    [ApiController]
    [Route("api/MovieDirection")]
    public class MovieDirectionsWriteController : ControllerBase, IMovieDirectionWriteApi
    {
        private readonly AppDbContext _context;

        public MovieDirectionsWriteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/movie_directions
        [HttpPost]
        [ProducesResponseType(typeof(MovieDirection), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<MovieDirection>> Create([FromBody] CreateMovieDirectionDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var directorExists = await _context.Directors.AnyAsync(d => d.DirId == dto.DirId);
            var movieExists    = await _context.Movies.AnyAsync(m => m.MovId == dto.MovId);
            if (!directorExists || !movieExists)
                return BadRequest("Director or Movie not found.");

            var exists = await _context.MovieDirections.AnyAsync(md => md.DirId == dto.DirId && md.MovId == dto.MovId);
            if (exists)
                return Conflict("This director is already linked to this movie.");

            var movieDirection = new MovieDirection { DirId = dto.DirId, MovId = dto.MovId };

            _context.MovieDirections.Add(movieDirection);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetMovieDirectionById",
                new { dirId = movieDirection.DirId, movId = movieDirection.MovId },
                movieDirection);
        }

        // PATCH: /api/movie_directions/{dirId}/{movId}
        [HttpPatch("{dirId:int}/{movId:int}")]
        [ProducesResponseType(400)]
        public Task<IActionResult> Patch(int dirId, int movId)
            => Task.FromResult<IActionResult>(
                BadRequest("movie_direction has no updatable fields; change of keys is not allowed."));

        // DELETE: /api/movie_directions/{dirId}/{movId}
        [HttpDelete("{dirId:int}/{movId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteById(int dirId, int movId)
        {
            var entity = await _context.MovieDirections.FindAsync(new object[] { dirId, movId });
            if (entity == null) return NotFound();

            _context.MovieDirections.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/movie_directions   (body: { "dirId": 5, "movId": 10 })
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByBody([FromBody] MovieDirectionDeleteDto dto)
        {
            var entity = await _context.MovieDirections.FindAsync(new object[] { dto.DirId, dto.MovId });
            if (entity == null) return NotFound();

            _context.MovieDirections.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
