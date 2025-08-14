using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieDirections.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.MovieDirection;

public partial class MovieDirectionWriteControllerTests : MovieDirectionTestBase
{
    [Fact, Trait("Area","MovieDirection"), Trait("Action","Patch")]
    public async Task Patch_Returns_BadRequest_Always()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "P", 2010);
        var d = SeedDirector(db, "Q", "R");
        Link(db, m.MovId, d.DirId);

        var sut = new MovieDirectionsWriteController(db);
        var res = await sut.Patch(m.MovId, d.DirId);

        var bad = Assert.IsType<BadRequestObjectResult>(res);
        Assert.Contains("no updatable fields", bad.Value?.ToString());
    }
    
}
