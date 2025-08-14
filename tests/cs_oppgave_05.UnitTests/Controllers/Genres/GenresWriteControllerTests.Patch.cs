using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Genres.Controllers;
using cs_oppgave_05.Api.Genres.Dtos;

namespace cs_oppgave_05.UnitTests.Controllers.Genres;

public partial class GenresWriteControllerTests : GenresTestBase
{
    [Fact, Trait("Area","Genres"), Trait("Action","Patch")]
    public async Task Patch_Updates_Title_When_Provided()
    {
        using var db = CreateDb();
        var g = SeedGenre(db, "Old");
        var sut = new GenreWriteController(db);

        var res = await sut.Patch(g.GenId, new UpdateGenreDto { GenTitle = "New" });
        Assert.IsType<NoContentResult>(res);

        var fromDb = await db.Genres.FindAsync(g.GenId);
        Assert.Equal("New", fromDb!.GenTitle);
    }

    [Fact, Trait("Area","Genres"), Trait("Action","Patch")]
    public async Task Patch_Returns_NotFound_For_Missing()
    {
        using var db = CreateDb();
        var sut = new GenreWriteController(db);

        var res = await sut.Patch(123456, new UpdateGenreDto { GenTitle = "X" });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
