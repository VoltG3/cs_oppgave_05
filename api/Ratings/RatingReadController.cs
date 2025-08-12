using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;
using cs_oppgave_05.Api.Ratings.Contracts;
using cs_oppgave_05.Data;

namespace cs_oppgave_05.Api.Ratings
{
    [ApiController]
    [Route("api/ratings")]
    public class RatingsReadController : ControllerBase, IRatingReadApi
    {
        private readonly AppDbContext _context;

        public RatingsReadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/ratings
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Rating>), 200)]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAll([FromQuery] int? movId, [FromQuery] int? revId)
        {
            var query = _context.Ratings
                .Include(r => r.Movie)
                .Include(r => r.Reviewer)
                .AsNoTracking()
                .AsQueryable();

            if (movId.HasValue && revId.HasValue)
            {
                var one = await query.FirstOrDefaultAsync(r => r.MovId == movId && r.RevId == revId);
                if (one == null) return NotFound();
                return Ok(one);
            }

            var list = await query.ToListAsync();
            return Ok(list);
        }

        // GET: /api/ratings/{movId}/{revId}
        [HttpGet("{movId:int}/{revId:int}", Name = "GetRatingById")]
        [ProducesResponseType(typeof(Rating), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Rating>> GetById(int movId, int revId)
        {
            var rating = await _context.Ratings
                .Include(r => r.Movie)
                .Include(r => r.Reviewer)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.MovId == movId && r.RevId == revId);

            if (rating == null) return NotFound();
            return Ok(rating);
        }
    }
    
}
