using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Api.Movies.Controllers; 
using cs_oppgave_05.Api.MovieGenres.Controllers; 
using cs_oppgave_05.Api.MovieDirections.Controllers;
using cs_oppgave_05.Api.MovieCasts.Controllers;
using cs_oppgave_05.Api.Ratings.Controllers;
using cs_oppgave_05.Api.MovieGenres.Dtos;
using cs_oppgave_05.Api.MovieDirections.Dtos;
using cs_oppgave_05.Api.MovieCasts.Dtos;
using cs_oppgave_05.Api.Ratings.Dtos;
using cs_oppgave_05.Api.Movies.Dtos;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Scenarios.MovieDetails;

public class MovieDetails_Delete_With_Relations : MovieDetailsTestBase
{
    [Fact, Trait("Type","Scenario"), Trait("Step","Delete")]
    public async Task DeleteMovie_With_All_Relations_Removes_Joins_But_Keeps_Master_Tables()
    {
        using var db = CreateRelationalDb();
        var (genre, director, actor, reviewer) = SeedMaster(db);

        var movie = Assert.IsType<Movie>(Assert.IsType<CreatedAtRouteResult>(
            (await new cs_oppgave_05.Api.Movies.Controllers.MoviesWriteController(db)
                .Create(new CreateMovieDto { MovTitle="Initial", MovYear=1935, MovRelCountry="DE" })).Result).Value);

        await new MovieGenresWriteController(db).Create(new CreateMovieGenresDto { MovId = movie.MovId, GenId = genre.GenId });
        await new MovieDirectionsWriteController(db).Create(new CreateMovieDirectionDto { MovId = movie.MovId, DirId = director.DirId });
        await new MovieCastsWriteController(db).Create(new CreateMovieCastDto { MovId = movie.MovId, ActId = actor.ActId, Role = "Pilot" });
        await new RatingsWriteController(db).Create(new CreateRatingDto { MovId = movie.MovId, RevId = reviewer.RevId, RevStars = 5, NumOfRatings = 1 });

        var del = await new MovieDeleteController(db).DeleteMovieWithAllRelations(movie.MovId);
        Assert.IsType<NoContentResult>(del);

        Assert.Null(await db.Movies.FindAsync(movie.MovId));
        Assert.Empty(await db.MovieGenres.ToListAsync());
        Assert.Empty(await db.MovieDirections.ToListAsync());
        Assert.Empty(await db.MovieCasts.ToListAsync());
        Assert.Empty(await db.Ratings.ToListAsync());

        // Master tables are still there
        Assert.NotNull(await db.Genres.FindAsync(genre.GenId));
        Assert.NotNull(await db.Directors.FindAsync(director.DirId));
        Assert.NotNull(await db.Actors.FindAsync(actor.ActId));
        Assert.NotNull(await db.Reviewers.FindAsync(reviewer.RevId));
    }
    
}
