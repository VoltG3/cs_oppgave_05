using Microsoft.AspNetCore.Mvc; 
using cs_oppgave_05.Api.MovieDirections.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.MovieDirection;

public partial class MovieDirectionReadControllerTests : MovieDirectionTestBase
{
    [Fact, Trait("Area","MovieDirection"), Trait("Action","GetAll")]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateDb();
        var m1 = SeedMovie(db, "A", 1999);
        var m2 = SeedMovie(db, "B", 2000);
        var d1 = SeedDirector(db, "Ann", "One");
        var d2 = SeedDirector(db, "Bob", "Two");

        Link(db, m1.MovId, d1.DirId);
        Link(db, m2.MovId, d2.DirId);

        var sut = new MovieDirectionsReadController(db);

        var res = await sut.GetAll(movId: null, dirId: null);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<Entities.MovieDirection>>(ok.Value);
        Assert.Equal(2, list.Count());
    }

    [Fact, Trait("Area","MovieDirection"), Trait("Action","GetAll")]
    public async Task GetAll_With_MovId_And_DirId_Returns_Single()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "One", 2024);
        var d = SeedDirector(db, "Solo", "Director");
        Link(db, m.MovId, d.DirId);

        var sut = new MovieDirectionsReadController(db);

        var res = await sut.GetAll(movId: m.MovId, dirId: d.DirId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var md = Assert.IsType<Entities.MovieDirection>(ok.Value);

        Assert.Equal(m.MovId, md.MovId);
        Assert.Equal(d.DirId, md.DirId);
    }

    [Fact, Trait("Area","MovieDirection"), Trait("Action","GetAll")]
    public async Task GetAll_With_MovId_And_DirId_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieDirectionsReadController(db);

        var res = await sut.GetAll(movId: 999, dirId: 888);
        Assert.IsType<NotFoundResult>(res.Result);
    }
    
}
