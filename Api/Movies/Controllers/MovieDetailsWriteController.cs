using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Api.Movies.Contracts;
using cs_oppgave_05.Api.Movies.Dtos;

namespace cs_oppgave_05.Api.Movies.Controllers
{
    [ApiController]
    [Route("api/movies/{id}/details")]
    public class MovieDetailsWriteController : ControllerBase, IMovieDetailsWriteApi
    {
        private readonly AppDbContext _context;

        public MovieDetailsWriteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPatch]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PatchDetails(int id, [FromBody] UpdateMovieDetailsDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Update movie
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null) return NotFound();

                if (dto.MovTitle is not null) movie.MovTitle = dto.MovTitle;
                if (dto.MovYear.HasValue) movie.MovYear = dto.MovYear.Value;
                if (dto.MovTime.HasValue) movie.MovTime = dto.MovTime.Value;
                if (dto.MovLang is not null) movie.MovLang = dto.MovLang;
                if (dto.MovDtRel.HasValue) movie.MovDtRel = dto.MovDtRel.Value;
                if (dto.MovRelCountry is not null) movie.MovRelCountry = dto.MovRelCountry;

                // Update genres
                if (dto.Genres != null)
                {
                    foreach (var genreDto in dto.Genres)
                    {
                        var genre = await _context.Genres.FindAsync(genreDto.GenId);
                        if (genre != null && genreDto.GenTitle is not null)
                        {
                            genre.GenTitle = genreDto.GenTitle;
                        }
                    }
                }

                // Update directors
                if (dto.Directors != null)
                {
                    foreach (var directorDto in dto.Directors)
                    {
                        var director = await _context.Directors.FindAsync(directorDto.DirId);
                        if (director != null)
                        {
                            if (directorDto.DirFname is not null) director.DirFname = directorDto.DirFname;
                            if (directorDto.DirLname is not null) director.DirLname = directorDto.DirLname;
                        }
                    }
                }

                // Update cast (actors and roles)
                if (dto.Cast != null)
                {
                    foreach (var castDto in dto.Cast)
                    {
                        var actor = await _context.Actors.FindAsync(castDto.ActId);
                        if (actor != null)
                        {
                            if (castDto.ActFname is not null) actor.ActFname = castDto.ActFname;
                            if (castDto.ActLname is not null) actor.ActLname = castDto.ActLname;
                        }

                        var movieCast = await _context.MovieCasts
                            .FirstOrDefaultAsync(mc => mc.MovId == id && mc.ActId == castDto.ActId);
                        if (movieCast != null && castDto.Role is not null)
                        {
                            movieCast.Role = castDto.Role;
                        }
                    }
                }

                // Update ratings
                if (dto.Ratings != null)
                {
                    foreach (var ratingDto in dto.Ratings)
                    {
                        var rating = await _context.Ratings
                            .FirstOrDefaultAsync(r => r.MovId == id && r.RevId == ratingDto.RevId);
                        if (rating != null)
                        {
                            if (ratingDto.RevStars.HasValue) rating.RevStars = ratingDto.RevStars.Value;
                            if (ratingDto.NumOfRatings.HasValue) rating.NumOfRatings = ratingDto.NumOfRatings.Value;
                        }
                    }
                }

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
