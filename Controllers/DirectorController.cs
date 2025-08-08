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
    }
}
