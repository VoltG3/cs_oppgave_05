using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieDirectionController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public MovieDirectionController(AppDbContext context)
        {
            _context = context;
        }
        
        // Get: api/movie_directions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDirection>>> GetAll()
        {
            var result = await _context.MovieDirections
                .Include(md => md.Movie)
                .Include(md => md.Director)
                .ToListAsync();

            return Ok(result);
        }
    }
}