using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;
using cs_oppgave_05.Api.MovieCasts.Contracts;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.MovieCasts.Dtos;

namespace cs_oppgave_05.Api.MovieCasts
{
    [ApiController]
    [Route("api/movie_casts")]
    public class MovieCastsWriteController : ControllerBase, IMovieCastWriteApi
    {
        private readonly AppDbContext _context;

        public MovieCastsWriteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/movie_casts
        [HttpPost]
        [ProducesResponseType(typeof(MovieCast), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<MovieCast>> Create([FromBody] CreateMovieCastDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            // Check Actor and Movie existence
            var actorExists = await _context.Actors.AnyAsync(a => a.ActId == dto.ActId);
            var movieExists = await _context.Movies.AnyAsync(m => m.MovId == dto.MovId);
            if (!actorExists || !movieExists)
                return BadRequest("Actor or Movie not found.");

            // Check for duplicate link
            var exists = await _context.MovieCasts.AnyAsync(mc => mc.ActId == dto.ActId && mc.MovId == dto.MovId);
            if (exists)
                return Conflict("This actor is already linked to this movie.");

            var movieCast = new MovieCast
            {
                ActId = dto.ActId,
                MovId = dto.MovId,
                Role  = dto.Role
            };

            _context.MovieCasts.Add(movieCast);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetMovieCastById", new { actId = movieCast.ActId, movId = movieCast.MovId }, movieCast);
        }

        // PATCH: /api/movie_casts/{actId}/{movId}
        [HttpPatch("{actId:int}/{movId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int actId, int movId, [FromBody] UpdateMovieCastDto dto)
        {
            var mc = await _context.MovieCasts.FirstOrDefaultAsync(x => x.ActId == actId && x.MovId == movId);
            if (mc == null) return NotFound();

            if (dto.Role is not null) mc.Role = dto.Role;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/movie_casts/{actId}/{movId}
        [HttpDelete("{actId:int}/{movId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteById(int actId, int movId)
        {
            var entity = await _context.MovieCasts.FindAsync(new object[] { actId, movId });
            if (entity == null) return NotFound();

            _context.MovieCasts.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/movie_casts   (body: { "actId": 5, "movId": 10 })
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByBody([FromBody] MovieCastDeleteDto dto)
        {
            var entity = await _context.MovieCasts.FindAsync(new object[] { dto.ActId, dto.MovId });
            if (entity == null) return NotFound();

            _context.MovieCasts.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
