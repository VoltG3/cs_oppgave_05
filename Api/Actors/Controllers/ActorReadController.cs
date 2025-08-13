using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.Actors.Contracts;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.Actors
{
    [ApiController]
    [Route("api/actors")]
    public class ActorReadController : ControllerBase, IActorReadApi
    {
        private readonly AppDbContext _context;

        public ActorReadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/actors
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Actor>), 200)]
        public async Task<ActionResult<IEnumerable<Actor>>> GetAll()
        {
            var actors = await _context.Actors
                .AsNoTracking()
                .ToListAsync();
            return Ok(actors);
        }

        // GET: /api/actors/{id}
        [HttpGet("{id}", Name = "GetActorById")]
        [ProducesResponseType(typeof(Actor), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Actor>> GetById(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null) return NotFound();
            return Ok(actor);
        }
    }
    
}
