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

public class MovieDetails_Read_Details : MovieDetailsTestBase
{
    [Fact, Trait("Type","Scenario"), Trait("Step","Read")]
    public async Task GetMovieDetails_Returns_All_Collections_With_Patched_Values()
    {
        using var db = CreateRelationalDb();
        var (genre, director, actor, reviewer) = SeedMaster(db);

        // Create + link
        var moviesWrite = new MoviesWriteController(db);
        var movie = Assert.IsType<Movie>(Assert.IsType<CreatedAtRouteResult>(
            (await moviesWrite.Create(new CreateMovieDto { MovTitle="Initial", MovYear=1935, MovRelCountry="DE" })).Result).Value);

        await new MovieGenresWriteController(db).Create(new CreateMovieGenresDto { MovId = movie.MovId, GenId = genre.GenId });
        await new MovieDirectionsWriteController(db).Create(new CreateMovieDirectionDto { MovId = movie.MovId, DirId = director.DirId });
        await new MovieCastsWriteController(db).Create(new CreateMovieCastDto { MovId = movie.MovId, ActId = actor.ActId, Role = "Pilot" });
        await new RatingsWriteController(db).Create(new CreateRatingDto { MovId = movie.MovId, RevId = reviewer.RevId, RevStars = 5, NumOfRatings = 1 });

        // Patch details
        await new MovieDetailsWriteController(db).PatchDetails(movie.MovId, new UpdateMovieDetailsDto {
            MovTitle = "_ovie_PACHED_929",
            MovRelCountry = "NO",
            Genres    = new() { new UpdateGenreDto(genre.GenId, "_enre_PACHED_929") },
            Directors = new() { new UpdateDirectorDto(director.DirId, "_irFname_PATCHED_929", "_irLname_929") },
            Cast      = new() { new UpdateCastDto(actor.ActId, "_ctorFname_PATCH_929", "_ctorLname_929", "_ole_929") },
            Ratings   = new() { new UpdateRatingDto(reviewer.RevId, 4.8m, 10) }
        });

        // Read details
        var moviesRead = new MoviesReadController(db);
        var result = await moviesRead.GetMovieDetails(movie.MovId, "genres,directors,cast,ratings");
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var details = Assert.IsType<MovieDetailsDto>(ok.Value);

        Assert.Equal(movie.MovId, details.MovId);
        Assert.Equal("_ovie_PACHED_929", details.MovTitle);
        Assert.Equal("NO", details.MovRelCountry);

        Assert.Single(details.Genres);
        Assert.Equal("_enre_PACHED_929", details.Genres[0].GenTitle);

        Assert.Single(details.Directors);
        Assert.Equal("_irFname_PATCHED_929", details.Directors[0].DirFname);
        Assert.Equal("_irLname_929",        details.Directors[0].DirLname);

        Assert.Single(details.Cast);
        Assert.Equal("_ctorFname_PATCH_929", details.Cast[0].ActFname);
        Assert.Equal("_ctorLname_929",      details.Cast[0].ActLname);
        Assert.Equal("_ole_929",            details.Cast[0].Role);

        Assert.Single(details.Ratings);
        Assert.Equal(4.8m, details.Ratings[0].RevStars);
        Assert.Equal(10,    details.Ratings[0].NumOfRatings);
    }
    
}
