using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.Ratings.Contracts;
using cs_oppgave_05.Api.Ratings.Dtos;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.Ratings.Controllers
{
    [ApiController]
    [Route("api/Ratings")]
    public class RatingsWriteController : ControllerBase, IRatingWriteApi
    {
        private readonly AppDbContext _context;

        public RatingsWriteController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/ratings
        [HttpPost]
        [ProducesResponseType(typeof(Rating), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Rating>> Create([FromBody] CreateRatingDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var movieExists = await _context.Movies.AnyAsync(m => m.MovId == dto.MovId);
            var reviewerExists = await _context.Reviewers.AnyAsync(r => r.RevId == dto.RevId);
            if (!movieExists || !reviewerExists)
                return BadRequest("Movie or Reviewer not found.");

            var exists = await _context.Ratings.AnyAsync(r => r.MovId == dto.MovId && r.RevId == dto.RevId);
            if (exists)
                return Conflict("This reviewer has already rated this movie.");

            var rating = new Rating
            {
                MovId = dto.MovId,
                RevId = dto.RevId,
                RevStars = dto.RevStars,
                NumOfRatings = dto.NumOfRatings
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetRatingById", new { movId = rating.MovId, revId = rating.RevId }, rating);
        }

        // PATCH: /api/ratings/{movId}/{revId}
        [HttpPatch("{movId:int}/{revId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(int movId, int revId, [FromBody] UpdateRatingDto dto)
        {
            var r = await _context.Ratings.FirstOrDefaultAsync(x => x.MovId == movId && x.RevId == revId);
            if (r == null) return NotFound();

            if (dto.RevStars.HasValue)     r.RevStars = dto.RevStars.Value;
            if (dto.NumOfRatings.HasValue) r.NumOfRatings = dto.NumOfRatings.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/ratings/{movId}/{revId}
        [HttpDelete("{movId:int}/{revId:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteById(int movId, int revId)
        {
            var entity = await _context.Ratings.FindAsync(new object[] { movId, revId });
            if (entity == null) return NotFound();

            _context.Ratings.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/ratings  (body: { "movId": 10, "revId": 3 })
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteByBody([FromBody] RatingDeleteDto dto)
        {
            var entity = await _context.Ratings.FindAsync(new object[] { dto.MovId, dto.RevId });
            if (entity == null) return NotFound();

            _context.Ratings.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
