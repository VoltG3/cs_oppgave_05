using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;
using cs_oppgave_05.Api.Directors.Contracts;
using cs_oppgave_05.Data;

namespace cs_oppgave_05.Api.Directors
{
    [ApiController]
    [Route("api/directors")]
    public class DirectorsReadController : ControllerBase, IDirectorReadApi
    {
        private readonly AppDbContext _context;

        public DirectorsReadController(AppDbContext context) => _context = context;

        // GET: /api/directors
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Director>), 200)]
        public async Task<ActionResult<IEnumerable<Director>>> GetAll()
        {
            var directors = await _context.Directors.AsNoTracking().ToListAsync();
            return Ok(directors);
        }

        // GET: /api/directors/{id}
        [HttpGet("{id}", Name = "GetDirectorById")]
        [ProducesResponseType(typeof(Director), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Director>> GetById(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null) return NotFound();
            return Ok(director);
        }
    }
    
}
