using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Models;
using cs_oppgave_05.Api.Reviewers.Contracts;
using cs_oppgave_05.Data;

namespace cs_oppgave_05.Api.Reviewers
{
    [ApiController]
    [Route("api/reviewers")]
    public class ReviewersReadController : ControllerBase, IReviewerReadApi
    {
        private readonly AppDbContext _context;

        public ReviewersReadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/reviewers
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Reviewer>), 200)]
        public async Task<ActionResult<IEnumerable<Reviewer>>> GetAll()
        {
            var reviewers = await _context.Reviewers
                .AsNoTracking()
                .ToListAsync();
            return Ok(reviewers);
        }

        // GET: /api/reviewers/{id}
        [HttpGet("{id}", Name = "GetReviewerById")]
        [ProducesResponseType(typeof(Reviewer), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Reviewer>> GetById(int id)
        {
            var reviewer = await _context.Reviewers.FindAsync(id);
            if (reviewer == null) return NotFound();
            return Ok(reviewer);
        }
    }
    
}
