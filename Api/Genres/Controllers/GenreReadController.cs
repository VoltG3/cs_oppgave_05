using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.Genres.Contracts;

namespace cs_oppgave_05.Api.Genres.Controllers
{
    [ApiController]
    [Route("api/Genres")]
    public class GenresReadController : ControllerBase, IGenreReadApi
    {
        private readonly AppDbContext _context;

        public GenresReadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/genres
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Entities.Genres>), 200)]
        public async Task<ActionResult<IEnumerable<Entities.Genres>>> GetAll()
        {
            var genres = await _context.Genres
                .AsNoTracking()
                .ToListAsync();
            return Ok(genres);
        }

        // GET: /api/genres/{id}
        [HttpGet("{id}", Name = "GetGenreById")]
        [ProducesResponseType(typeof(Entities.Genres), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Entities.Genres>> GetById(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();
            return Ok(genre);
        }
    }
    
}
