using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public GenresController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genres>>> GetAll()
        {
            var genres = await _context.Genres.ToListAsync();
            return Ok(genres);
        }
    }
}