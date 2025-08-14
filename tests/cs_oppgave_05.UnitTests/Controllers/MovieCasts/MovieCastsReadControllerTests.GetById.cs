using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieCasts.Controllers; 
using cs_oppgave_05.Entities; 

namespace cs_oppgave_05.UnitTests.Controllers.MovieCasts;

public partial class MovieCastsReadControllerTests : MovieCastsTestBase
{
    [Fact, Trait("Area","MovieCasts"), Trait("Action","GetById")]
    public async Task GetById_Returns_Ok()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "X", 2005);
        var a = SeedActor(db, "Y", "Z", "M");
        Link(db, m.MovId, a.ActId, "Hero");

        var sut = new MovieCastsReadController(db);
        var res = await sut.GetById(m.MovId, a.ActId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var mc = Assert.IsType<MovieCast>(ok.Value);

        Assert.Equal(m.MovId, mc.MovId);
        Assert.Equal(a.ActId, mc.ActId);
        Assert.Equal("Hero", mc.Role);
    }

    [Fact, Trait("Area","MovieCasts"), Trait("Action","GetById")]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieCastsReadController(db);
        var res = await sut.GetById(123, 456);
        Assert.IsType<NotFoundResult>(res.Result);
    }
    
}
