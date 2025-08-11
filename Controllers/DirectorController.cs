using cs_oppgave_05.Data.DTOs.Actor;
using cs_oppgave_05.Data.DTOs.Directors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DirectorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/directors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> GetAll()
        {
            var directors = await _context.Directors.ToListAsync();
            return Ok(directors);
        }
        
        // GET: api/directors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> GetById(int id)
        {
            var director = await _context.Directors.FindAsync(id);

            if (director == null)
            {
                return NotFound();
            }

            return Ok(director);
        }
        
        // POST: api/directors
        [HttpPost]
        public async Task<ActionResult<Director>> Create([FromBody] CreateDirectorDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var director = new Director
            {
                DirFname = dto.DirFname,
                DirLname = dto.DirLname
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = director.DirId }, director);
        }
        
        // PATCH:
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] System.Text.Json.JsonElement changes)
        {
            var entity = await _context.Directors.FindAsync(id);
            if (entity == null) return NotFound();

            if (changes.TryGetProperty("dirFname", out var fn)) entity.DirFname = fn.GetString();
            if (changes.TryGetProperty("dirLname", out var ln)) entity.DirLname = ln.GetString();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Directors.FindAsync(id);
            if (entity == null) return NotFound();
            _context.Directors.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE DTO
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteByIdDto dto)
        {
            var entity = await _context.Directors.FindAsync(dto.Id);
            if (entity == null) return NotFound();
            _context.Directors.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
