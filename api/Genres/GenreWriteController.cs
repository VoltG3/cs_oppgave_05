using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Genres.Contracts;
using cs_oppgave_05.Data;
using cs_oppgave_05.Data.DTOs.Genres;

namespace cs_oppgave_05.Api.Genres
{
    [ApiController]
    [Route("api/genres")]
    public class GenreWriteController : ControllerBase, IGenreWriteApi
    {
        private readonly AppDbContext _context;

        public GenreWriteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/genres
        [HttpPost]
        [ProducesResponseType(typeof(Models.Genres), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Models.Genres>> Create([FromBody] CreateGenreDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var genre = new Models.Genres
            {
                GenTitle = dto.GenTitle
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetGenreById", new { id = genre.GenId }, genre);
        }

        // PATCH: /api/genres/{id}
        [HttpPatch("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateGenreDto dto)
        {
            var entity = await _context.Genres.FindAsync(id);
            if (entity == null) return NotFound();

            if (dto.GenTitle is not null) entity.GenTitle = dto.GenTitle;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/genres/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteById(int id)
        {
            var entity = await _context.Genres.FindAsync(id);
            if (entity == null) return NotFound();

            _context.Genres.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/genres  (body: { "id": 7 })
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByBody([FromBody] DeleteByIdDto dto)
        {
            var entity = await _context.Genres.FindAsync(dto.Id);
            if (entity == null) return NotFound();

            _context.Genres.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
