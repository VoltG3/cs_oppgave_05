using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Controllers; 
namespace cs_oppgave_05.UnitTests.Controllers.Movies;

public partial class MovieDeleteControllerTests : MoviesTestBase
{
    [Fact, Trait("Area","Movies"), Trait("Action","DeleteOnly")]
    public async Task DeleteOnly_Removes_Movie()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "Only", 2001);
        var sut = new MovieDeleteController(db); 

        var res = await sut.DeleteMovieOnly(m.MovId);
        Assert.IsType<NoContentResult>(res);

        var gone = await db.Movies.FindAsync(m.MovId);
        Assert.Null(gone);
    }

    [Fact, Trait("Area","Movies"), Trait("Action","DeleteOnly")]
    public async Task DeleteOnly_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieDeleteController(db);

        var res = await sut.DeleteMovieOnly(999);
        Assert.IsType<NotFoundResult>(res);
    }
    
}
