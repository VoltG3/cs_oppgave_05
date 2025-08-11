using cs_oppgave_05.Data.DTOs.Actor;
using cs_oppgave_05.Data.DTOs.Reviewers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewersController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ReviewersController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/reviewers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviewer>>> GetAll()
        {
            var reviewers = await _context.Directors.ToListAsync();
            return Ok(reviewers);
        }
        
        // GET: api/reviewers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Reviewer>> GetById(int id)
        {
            var reviewer = await _context.Reviewers.FindAsync(id);

            if (reviewer == null)
            {
                return NotFound();
            }

            return Ok(reviewer);
        }
        
        // POST: api/reviewers
        [HttpPost]
        public async Task<ActionResult<Reviewer>> Create([FromBody] CreateReviewerDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var reviewer = new Reviewer
            {
                RevName = dto.RevName
            };

            _context.Reviewers.Add(reviewer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = reviewer.RevId }, reviewer);
        }
        
        // PATCH:
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] System.Text.Json.JsonElement changes)
        {
            var entity = await _context.Reviewers.FindAsync(id);
            if (entity == null) return NotFound();

            if (changes.TryGetProperty("revName", out var n)) entity.RevName = n.GetString();

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Reviewers.FindAsync(id);
            if (entity == null) return NotFound();
            _context.Reviewers.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE DTO
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteByIdDto dto)
        {
            var entity = await _context.Reviewers.FindAsync(dto.Id);
            if (entity == null) return NotFound();
            _context.Reviewers.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}
