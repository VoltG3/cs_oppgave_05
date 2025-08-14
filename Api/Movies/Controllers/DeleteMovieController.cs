using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.Movies.Contracts;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.Api.Movies.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieDeleteController : ControllerBase, IDeleteMovieApi
    {
        private readonly AppDbContext _context;

        public MovieDeleteController(AppDbContext context)
        {
            _context = context;
        }

        // DELETE: api/movies/{id}/only
        [HttpDelete("{id}/only")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMovieOnly(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/movies/{id}/with-relations
        [HttpDelete("{id}/with-relations")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMovieWithAllRelations(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var movie = await _context.Movies
                    .Include(m => m.MovieGenres)
                    .Include(m => m.MovieDirections)
                    .Include(m => m.MovieCasts)
                    .Include(m => m.Ratings)
                    .FirstOrDefaultAsync(m => m.MovId == id);

                if (movie == null) return NotFound();

                // Delete related records
                // Delete related records (droši pret null)
                _context.MovieGenres.RemoveRange(movie.MovieGenres ?? Enumerable.Empty<Entities.MovieGenres>());
                _context.MovieDirections.RemoveRange(movie.MovieDirections ?? Enumerable.Empty<MovieDirection>());
                _context.MovieCasts.RemoveRange(movie.MovieCasts ?? Enumerable.Empty<MovieCast>());
                _context.Ratings.RemoveRange(movie.Ratings ?? Enumerable.Empty<Rating>());

                // Finally delete the movie
                _context.Movies.Remove(movie);


                // Finally delete the movie
                _context.Movies.Remove(movie);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return NoContent();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    
}
