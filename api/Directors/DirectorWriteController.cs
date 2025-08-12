using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;
using cs_oppgave_05.Api.Directors.Contracts;
using cs_oppgave_05.Data;
using cs_oppgave_05.Data.DTOs.Director;
using cs_oppgave_05.Data.DTOs.Directors;

namespace cs_oppgave_05.Api.Directors
{
    [ApiController]
    [Route("api/directors")]
    public class DirectorsWriteController : ControllerBase, IDirectorWriteApi
    {
        private readonly AppDbContext _context;

        public DirectorsWriteController(AppDbContext context) => _context = context;

        // POST: /api/directors
        [HttpPost]
        [ProducesResponseType(typeof(Director), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Director>> Create([FromBody] CreateDirectorDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var director = new Director { DirFname = dto.DirFname, DirLname = dto.DirLname };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetDirectorById", new { id = director.DirId }, director);
        }

        // PATCH: /api/directors/{id}
        [HttpPatch("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateDirectorDto dto)
        {
            var entity = await _context.Directors.FindAsync(id);
            if (entity == null) return NotFound();

            if (dto.DirFname is not null) entity.DirFname = dto.DirFname;
            if (dto.DirLname is not null) entity.DirLname = dto.DirLname;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/directors/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteById(int id)
        {
            var entity = await _context.Directors.FindAsync(id);
            if (entity == null) return NotFound();

            _context.Directors.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/directors  (body: { "id": 42 })
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByBody([FromBody] DeleteByIdDto dto)
        {
            var entity = await _context.Directors.FindAsync(dto.Id);
            if (entity == null) return NotFound();

            _context.Directors.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
