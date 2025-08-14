using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Entities;

// Controllers
using cs_oppgave_05.Api.Movies.Controllers;
using cs_oppgave_05.Api.MovieGenres.Controllers;
using cs_oppgave_05.Api.MovieDirections.Controllers;
using cs_oppgave_05.Api.MovieCasts.Controllers;
using cs_oppgave_05.Api.Ratings.Controllers;

// DTOs
using cs_oppgave_05.Api.Movies.Dtos;
using cs_oppgave_05.Api.MovieGenres.Dtos;
using cs_oppgave_05.Api.MovieDirections.Dtos;
using cs_oppgave_05.Api.MovieCasts.Dtos;
using cs_oppgave_05.Api.Ratings.Dtos;

namespace cs_oppgave_05.UnitTests.Scenarios.MovieDetails;

public class MovieDetails_Create_And_Link_All : MovieDetailsTestBase
{
    [Fact, Trait("Type","Scenario"), Trait("Step","Create+Link")]
    public async Task Create_Movie_And_Link_All_Relations()
    {
        using var db = CreateRelationalDb();
        var (genre, director, actor, reviewer) = SeedMaster(db);

        // POST movie
        var moviesWrite = new MoviesWriteController(db);
        var create = await moviesWrite.Create(new CreateMovieDto {
            MovTitle = "Initial Title", MovYear = 1935, MovTime = 110, MovLang = "DE", MovRelCountry = "DE"
        });
        var created = Assert.IsType<CreatedAtRouteResult>(create.Result);
        Assert.Equal("GetMovieById", created.RouteName);
        var movie = Assert.IsType<Movie>(created.Value);

        // Link all join records
        var mg = new MovieGenresWriteController(db);
        var md = new MovieDirectionsWriteController(db);
        var mc = new MovieCastsWriteController(db);
        var rt = new RatingsWriteController(db);

        Assert.IsType<CreatedAtRouteResult>((await mg.Create(new CreateMovieGenresDto { MovId = movie.MovId, GenId = genre.GenId })).Result);
        Assert.IsType<CreatedAtRouteResult>((await md.Create(new CreateMovieDirectionDto { MovId = movie.MovId, DirId = director.DirId })).Result);
        Assert.IsType<CreatedAtRouteResult>((await mc.Create(new CreateMovieCastDto { MovId = movie.MovId, ActId = actor.ActId, Role = "Pilot" })).Result);
        Assert.IsType<CreatedAtRouteResult>((await rt.Create(new CreateRatingDto { MovId = movie.MovId, RevId = reviewer.RevId, RevStars = 5, NumOfRatings = 1 })).Result);
    }
    
}
