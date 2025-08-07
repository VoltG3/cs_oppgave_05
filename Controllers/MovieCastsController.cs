using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieCastsController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public MovieCastsController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/movie_casts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieCast>>> GetAll()
        {
            var movieCasts = await _context.MovieCasts.ToListAsync();
            return Ok(movieCasts);
        }
        
        // GET: api/movie_casts/{id}/{id}
        [HttpGet("{actId}/{movId}")]
        public async Task<ActionResult<MovieCast>> GetById(int actId, int movId)
        {
            var movieCast = await _context.MovieCasts
                .FirstOrDefaultAsync(mk => mk.ActId == actId && mk.MovId == movId);

            if (movieCast == null)
            {
                return NotFound();
            }

            return Ok(movieCast);
        }
    }
}