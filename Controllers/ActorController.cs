using cs_oppgave_05.Data.DTOs.Actors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ActorsController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAll()
        {
            var actors = await _context.Actors.ToListAsync();
            return Ok(actors);
        }
        
        // GET: api/actors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetById(int id)
        {
            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor);
        }
        
        // POST: api/actors
        [HttpPost]
        public async Task<ActionResult<Actor>> Create([FromBody] CreateActorDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var actor = new Actor
            {
                ActFname = dto.ActFname,
                ActLname = dto.ActLname,
                ActGender = dto.ActGender
            };

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = actor.ActId }, actor);
        }

        // PATCH :
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] System.Text.Json.JsonElement changes)
        {
            var entity = await _context.Actors.FindAsync(id);
            if (entity == null) return NotFound();

            if (changes.TryGetProperty("actFname", out var fn)) entity.ActFname = fn.GetString();
            if (changes.TryGetProperty("actLname", out var ln)) entity.ActLname = ln.GetString();
            if (changes.TryGetProperty("actGender", out var g)) entity.ActGender = g.GetString();

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
