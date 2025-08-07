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
    }
}