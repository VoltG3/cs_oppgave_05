using cs_oppgave_05.Api._Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;
using cs_oppgave_05.Api.Actors.Contracts;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.Actors.Dtos;

namespace cs_oppgave_05.Api.Actors.Controllers
{
    [ApiController]
    [Route("api/Actors")]
    public class ActorsWriteController : ControllerBase, IActorWriteApi
    {
        private readonly AppDbContext _context;

        public ActorsWriteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/actors
        [HttpPost]
        [ProducesResponseType(typeof(Actor), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Actor>> Create([FromBody] CreateActorDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var actor = new Actor
            {
                ActFname = dto.ActFname,
                ActLname = dto.ActLname,
                ActGender = dto.ActGender
            };

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            // Atsauce uz lasīšanas kontrolieri maršrutu
            return CreatedAtRoute("GetActorById", new { id = actor.ActId }, actor);
        }

        // PATCH: /api/actors/{id}
        [HttpPatch("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateActorDto dto)
        {
            var entity = await _context.Actors.FindAsync(id);
            if (entity == null) return NotFound();

            // Pielieto tikai nenull vērtības
            if (dto.ActFname is not null) entity.ActFname = dto.ActFname;
            if (dto.ActLname is not null) entity.ActLname = dto.ActLname;
            if (dto.ActGender is not null) entity.ActGender = dto.ActGender;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/actors/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteById(int id)
        {
            var entity = await _context.Actors.FindAsync(id);
            if (entity == null) return NotFound();

            _context.Actors.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/actors (body: { "id": 42 })
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByBody([FromBody] DeleteByIdDto dto)
        {
            var entity = await _context.Actors.FindAsync(dto.Id);
            if (entity == null) return NotFound();

            _context.Actors.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
