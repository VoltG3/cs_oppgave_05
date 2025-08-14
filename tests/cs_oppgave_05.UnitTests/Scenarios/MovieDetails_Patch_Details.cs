using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Controllers; 
using cs_oppgave_05.Api.Movies.Dtos; 
using cs_oppgave_05.Api.MovieGenres.Controllers; 
using cs_oppgave_05.Api.MovieDirections.Controllers;
using cs_oppgave_05.Api.MovieCasts.Controllers;
using cs_oppgave_05.Api.Ratings.Controllers;
using cs_oppgave_05.Api.MovieGenres.Dtos;
using cs_oppgave_05.Api.MovieDirections.Dtos;
using cs_oppgave_05.Api.MovieCasts.Dtos;
using cs_oppgave_05.Api.Ratings.Dtos;
using cs_oppgave_05.Entities;
using UpdateRatingDto = cs_oppgave_05.Api.Movies.Dtos.UpdateRatingDto;

namespace cs_oppgave_05.UnitTests.Scenarios.MovieDetails;

public class MovieDetails_Patch_Details : MovieDetailsTestBase
{
    [Fact, Trait("Type","Scenario"), Trait("Step","Patch")]
    public async Task Patch_Details_Updates_Movie_And_All_Linked_Records()
    {
        using var db = CreateRelationalDb();
        var (genre, director, actor, reviewer) = SeedMaster(db);

        // Create + link
        var moviesWrite = new MoviesWriteController(db);
        var create = await moviesWrite.Create(new CreateMovieDto { MovTitle="Initial", MovYear=1935, MovRelCountry="DE" });
        var movie = Assert.IsType<Movie>(Assert.IsType<CreatedAtRouteResult>(create.Result).Value);

        await new MovieGenresWriteController(db).Create(new CreateMovieGenresDto { MovId = movie.MovId, GenId = genre.GenId });
        await new MovieDirectionsWriteController(db).Create(new CreateMovieDirectionDto { MovId = movie.MovId, DirId = director.DirId });
        await new MovieCastsWriteController(db).Create(new CreateMovieCastDto { MovId = movie.MovId, ActId = actor.ActId, Role = "Pilot" });
        await new RatingsWriteController(db).Create(new CreateRatingDto { MovId = movie.MovId, RevId = reviewer.RevId, RevStars = 5, NumOfRatings = 1 });

        // PATCH /api/movies/{id}/details (vienā solī)
        var detailsWrite = new MovieDetailsWriteController(db);
        var dto = new UpdateMovieDetailsDto
        {
            MovTitle = "_ovie_PACHED_929",
            MovRelCountry = "NO",
            Genres    = new() { new UpdateGenreDto(genre.GenId, "_enre_PACHED_929") },
            Directors = new() { new UpdateDirectorDto(director.DirId, "_irFname_PATCHED_929", "_irLname_929") },
            Cast      = new() { new UpdateCastDto(actor.ActId, "_ctorFname_PATCH_929", "_ctorLname_929", "_ole_929") },
            Ratings   = new() { new UpdateRatingDto(reviewer.RevId, 4.8m, 10) }
        };

        var res = await detailsWrite.PatchDetails(movie.MovId, dto);
        Assert.IsType<NoContentResult>(res);
    }
    
}
