using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieCasts.Controllers; 
using cs_oppgave_05.Api.MovieCasts.Dtos; 

namespace cs_oppgave_05.UnitTests.Controllers.MovieCasts;

public partial class MovieCastsWriteControllerTests : MovieCastsTestBase
{
    [Fact, Trait("Area","MovieCasts"), Trait("Action","Patch")]
    public async Task Patch_Updates_Role_When_Provided()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "X", 2010);
        var a = SeedActor(db, "Y", "Z", "M");
        Link(db, m.MovId, a.ActId, "Old");

        var sut = new MovieCastsWriteController(db);

        var res = await sut.Patch(m.MovId, a.ActId, new UpdateMovieCastDto { Role = "New" });
        Assert.IsType<NoContentResult>(res);

        var fromDb = await db.MovieCasts.FindAsync(m.MovId, a.ActId);
        Assert.NotNull(fromDb);
        Assert.Equal("New", fromDb!.Role);
    }

    [Fact, Trait("Area","MovieCasts"), Trait("Action","Patch")]
    public async Task Patch_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieCastsWriteController(db);

        var res = await sut.Patch(111, 222, new UpdateMovieCastDto { Role = "X" });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
