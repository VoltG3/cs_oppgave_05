using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public RatingsController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAll()
        {
            var ratings = await _context.Ratings.ToListAsync();
            return Ok(ratings);
        }
        
        // GET: api/ratings/{id}/{id}
        [HttpGet("{movId}/{revId}")]
        public async Task<ActionResult<Rating>> GetById(int movId, int revId)
        {
            var rating = await _context.Ratings
                .FirstOrDefaultAsync(r => r.MovId == movId && r.RevId == revId);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }
    }
}