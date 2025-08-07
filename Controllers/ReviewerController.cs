using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewersController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ReviewersController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/reviewers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviewer>>> GetAll()
        {
            var reviewers = await _context.Directors.ToListAsync();
            return Ok(reviewers);
        }
    }
}