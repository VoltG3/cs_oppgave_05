using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using cs_oppgave_05.Api.Movies.Controllers;

namespace cs_oppgave_05.UnitTests.Controllers.Movies;

public partial class MovieDeleteControllerTests : MoviesTestBase
{
    [Fact, Trait("Area","Movies"), Trait("Action","DeleteWithRelations"), Trait("DB","Relational")]
    public async Task DeleteWithRelations_Removes_Movie_And_Related()
    {
        using var db = CreateRelationalDb(); 
        var m = SeedMovie(db, "Rel", 1990);
        SeedRelationsFor(db, m);

        var sut = new MovieDeleteController(db);
        var res = await sut.DeleteMovieWithAllRelations(m.MovId);
        Assert.IsType<NoContentResult>(res);

        Assert.Null(await db.Movies.FindAsync(m.MovId));
        Assert.Empty(await db.MovieGenres.ToListAsync());
        Assert.Empty(await db.MovieDirections.ToListAsync());
        Assert.Empty(await db.MovieCasts.ToListAsync());
        Assert.Empty(await db.Ratings.ToListAsync());
    }

    [Fact, Trait("Area","Movies"), Trait("Action","DeleteWithRelations"), Trait("DB","Relational")]
    public async Task DeleteWithRelations_Returns_NotFound_When_Missing()
    {
        using var db = CreateRelationalDb(); 
        var sut = new MovieDeleteController(db);

        var res = await sut.DeleteMovieWithAllRelations(999);
        Assert.IsType<NotFoundResult>(res);
    }
    
}
