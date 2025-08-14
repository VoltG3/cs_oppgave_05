using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Controllers;
using cs_oppgave_05.Api.Movies.Dtos;

namespace cs_oppgave_05.UnitTests.Controllers.Movies;

public partial class MoviesWriteControllerTests : MoviesTestBase
{
    [Fact, Trait("Area","Movies"), Trait("Action","Patch")]
    public async Task Patch_Updates_Fields_When_Provided()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "Old", 2000);
        var sut = new MoviesWriteController(db);

        var res = await sut.Patch(m.MovId, new UpdateMovieDto
        {
            MovTitle = "New",
            MovYear = 2025,
            MovLang = "NO"
        });
        Assert.IsType<NoContentResult>(res);

        var fromDb = await db.Movies.FindAsync(m.MovId);
        Assert.Equal("New", fromDb!.MovTitle);
        Assert.Equal(2025, fromDb.MovYear);
        Assert.Equal("NO", fromDb.MovLang);
    }

    [Fact, Trait("Area","Movies"), Trait("Action","Patch")]
    public async Task Patch_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new MoviesWriteController(db);

        var res = await sut.Patch(123456, new UpdateMovieDto { MovTitle = "X" });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
