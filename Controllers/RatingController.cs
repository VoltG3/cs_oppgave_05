using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;
using cs_oppgave_05.Data.DTOs.Rating;

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
        
        // POST: api/ratings
        [HttpPost]
        public async Task<ActionResult<Rating>> Create([FromBody] CreateRatingDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            // CHECK – exist Movie and Reviewer
            var movieExists = await _context.Movies.AnyAsync(m => m.MovId == dto.MovId);
            var reviewerExists = await _context.Reviewers.AnyAsync(r => r.RevId == dto.RevId);
            if (!movieExists || !reviewerExists)
                return BadRequest("Movie or Reviewer not found.");

            // Check – exist link
            var exists = await _context.Ratings
                .AnyAsync(r => r.MovId == dto.MovId && r.RevId == dto.RevId);
            if (exists)
                return Conflict("This reviewer has already rated this movie.");

            // Create new record
            var rating = new Rating
            {
                MovId = dto.MovId,
                RevId = dto.RevId,
                RevStars = dto.RevStars,
                NumOfRatings = dto.NumOfRatings
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { movId = rating.MovId, revId = rating.RevId },
                rating);
        }

    }
}