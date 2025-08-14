using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieDirections.Controllers;

namespace cs_oppgave_05.UnitTests.Controllers.MovieDirection;

public partial class MovieDirectionReadControllerTests : MovieDirectionTestBase
{
    [Fact, Trait("Area","MovieDirection"), Trait("Action","GetById")]
    public async Task GetById_Returns_Ok()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "X", 2005);
        var d = SeedDirector(db, "Y", "Z");
        Link(db, m.MovId, d.DirId);

        var sut = new MovieDirectionsReadController(db);
        var res = await sut.GetById(m.MovId, d.DirId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var md = Assert.IsType<Entities.MovieDirection>(ok.Value);

        Assert.Equal(m.MovId, md.MovId);
        Assert.Equal(d.DirId, md.DirId);
    }

    [Fact, Trait("Area","MovieDirection"), Trait("Action","GetById")]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieDirectionsReadController(db);
        var res = await sut.GetById(123, 456);
        Assert.IsType<NotFoundResult>(res.Result);
    }
    
}
