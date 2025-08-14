using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieGenres.Controllers;

namespace cs_oppgave_05.UnitTests.Controllers.MovieGenres;

public partial class MovieGenresWriteControllerTests : MovieGenresTestBase
{
    [Fact, Trait("Area","MovieGenres"), Trait("Action","Patch")]
    public async Task Patch_Returns_BadRequest_Always()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "P", 2010);
        var g = SeedGenre(db, "Q");
        Link(db, m.MovId, g.GenId);

        var sut = new MovieGenresWriteController(db);
        var res = await sut.Patch(m.MovId, g.GenId);

        var bad = Assert.IsType<BadRequestObjectResult>(res);
        Assert.Contains("no updatable fields", bad.Value?.ToString());
    }
    
}
