using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieGenresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovieGenresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/moviegenres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieGenres>>> GetAll()
        {
            var movieGenres = await _context.MovieGenress.ToListAsync();
            return Ok(movieGenres);
        }
    }
}