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
        public async Task<ActionResult<Actor>> Create([FromBody] Actor actor)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = actor.ActId }, actor);
        }

    }
}
