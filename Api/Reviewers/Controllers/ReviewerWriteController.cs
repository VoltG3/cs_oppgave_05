using cs_oppgave_05.Api._Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;
using cs_oppgave_05.Api.Reviewers.Contracts;
using cs_oppgave_05.Api.Reviewers.Dtos;
using cs_oppgave_05.Data;

namespace cs_oppgave_05.Api.Reviewers
{
    [ApiController]
    [Route("api/reviewers")]
    public class ReviewersWriteController : ControllerBase, IReviewerWriteApi
    {
        private readonly AppDbContext _context;

        public ReviewersWriteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/reviewers
        [HttpPost]
        [ProducesResponseType(typeof(Reviewer), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Reviewer>> Create([FromBody] CreateReviewerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var reviewer = new Reviewer { RevName = dto.RevName };

            _context.Reviewers.Add(reviewer);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetReviewerById", new { id = reviewer.RevId }, reviewer);
        }

        // PATCH: /api/reviewers/{id}
        [HttpPatch("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateReviewerDto dto)
        {
            var entity = await _context.Reviewers.FindAsync(id);
            if (entity == null) return NotFound();

            if (dto.RevName is not null) entity.RevName = dto.RevName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/reviewers/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteById(int id)
        {
            var entity = await _context.Reviewers.FindAsync(id);
            if (entity == null) return NotFound();

            _context.Reviewers.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/reviewers   (body: { "id": 5 })
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByBody([FromBody] DeleteByIdDto dto)
        {
            var entity = await _context.Reviewers.FindAsync(dto.Id);
            if (entity == null) return NotFound();

            _context.Reviewers.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
