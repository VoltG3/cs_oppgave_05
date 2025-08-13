using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.Movies.Contracts;
using cs_oppgave_05.Api.Movies.Dtos;
using cs_oppgave_05.Entities;

using DeleteByIdDto = cs_oppgave_05.Api._Shared.Dtos.DeleteByIdDto;

namespace cs_oppgave_05.Api.Movies
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesWriteController : ControllerBase, IMovieWriteApi
    {
        private readonly AppDbContext _context;

        public MoviesWriteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/movies
        [HttpPost]
        [ProducesResponseType(typeof(Movie), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Movie>> Create([FromBody] CreateMovieDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

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

            // Use the named route from MoviesReadController for Location header.
            return CreatedAtRoute("GetMovieById", new { id = movie.MovId }, movie);
        }

        // PATCH: /api/movies/{id}
        [HttpPatch("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateMovieDto dto)
        {
            var entity = await _context.Movies.FindAsync(id);
            if (entity == null) return NotFound();

            // Only apply non-null values (partial update).
            if (dto.MovTitle is not null) entity.MovTitle = dto.MovTitle;
            if (dto.MovYear.HasValue)      entity.MovYear = dto.MovYear.Value;
            if (dto.MovTime.HasValue)      entity.MovTime = dto.MovTime.Value;
            if (dto.MovLang is not null)   entity.MovLang = dto.MovLang;
            if (dto.MovDtRel.HasValue)     entity.MovDtRel = dto.MovDtRel.Value;
            if (dto.MovRelCountry is not null) entity.MovRelCountry = dto.MovRelCountry;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/movies/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteById(int id)
        {
            var entity = await _context.Movies.FindAsync(id);
            if (entity == null) return NotFound();

            _context.Movies.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/movies (body: { "id": 42 })
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByBody([FromBody] DeleteByIdDto dto)
        {
            var entity = await _context.Movies.FindAsync(dto.Id);
            if (entity == null) return NotFound();

            _context.Movies.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
