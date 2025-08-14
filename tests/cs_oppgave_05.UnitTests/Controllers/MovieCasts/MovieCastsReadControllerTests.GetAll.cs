using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieCasts.Controllers; 
using cs_oppgave_05.Entities; 

namespace cs_oppgave_05.UnitTests.Controllers.MovieCasts;

public partial class MovieCastsReadControllerTests : MovieCastsTestBase
{
    [Fact, Trait("Area","MovieCasts"), Trait("Action","GetAll")]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateDb();
        var m1 = SeedMovie(db, "A", 1999);
        var m2 = SeedMovie(db, "B", 2000);
        var a1 = SeedActor(db, "Ann", "One", "F");
        var a2 = SeedActor(db, "Bob", "Two", "M");

        Link(db, m1.MovId, a1.ActId, "Lead");
        Link(db, m2.MovId, a2.ActId, "Support");

        var sut = new MovieCastsReadController(db);

        var res = await sut.GetAll(movId: null, actId: null);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<MovieCast>>(ok.Value);
        Assert.Equal(2, list.Count());
    }

    [Fact, Trait("Area","MovieCasts"), Trait("Action","GetAll")]
    public async Task GetAll_With_MovId_And_ActId_Returns_Single()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "One", 2024);
        var a = SeedActor(db, "Solo", "Star", "F");
        Link(db, m.MovId, a.ActId, "Hero");

        var sut = new MovieCastsReadController(db);

        var res = await sut.GetAll(movId: m.MovId, actId: a.ActId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var mc = Assert.IsType<MovieCast>(ok.Value);

        Assert.Equal(m.MovId, mc.MovId);
        Assert.Equal(a.ActId, mc.ActId);
        Assert.Equal("Hero", mc.Role);
    }

    [Fact, Trait("Area","MovieCasts"), Trait("Action","GetAll")]
    public async Task GetAll_With_MovId_And_ActId_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieCastsReadController(db);

        var res = await sut.GetAll(movId: 999, actId: 888);
        Assert.IsType<NotFoundResult>(res.Result);
    }
    
}
