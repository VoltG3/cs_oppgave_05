using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieGenres.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.MovieGenres;

public partial class MovieGenresReadControllerTests : MovieGenresTestBase
{
    [Fact, Trait("Area","MovieGenres"), Trait("Action","GetAll")]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateDb();
        var m1 = SeedMovie(db, "A", 1999);
        var m2 = SeedMovie(db, "B", 2000);
        var g1 = SeedGenre(db, "Drama");
        var g2 = SeedGenre(db, "Comedy");

        Link(db, m1.MovId, g1.GenId);
        Link(db, m2.MovId, g2.GenId);

        var sut = new MovieGenresReadController(db);

        var res = await sut.GetAll(movId: null, genId: null);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<Entities.MovieGenres>>(ok.Value);
        Assert.Equal(2, list.Count());
    }

    [Fact, Trait("Area","MovieGenres"), Trait("Action","GetAll")]
    public async Task GetAll_With_MovId_And_GenId_Returns_Single()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "One", 2001);
        var g = SeedGenre(db, "Action");
        Link(db, m.MovId, g.GenId);

        var sut = new MovieGenresReadController(db);

        var res = await sut.GetAll(movId: m.MovId, genId: g.GenId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var mg = Assert.IsType<Entities.MovieGenres>(ok.Value);
        Assert.Equal(m.MovId, mg.MovId);
        Assert.Equal(g.GenId, mg.GenId);
    }

    [Fact, Trait("Area","MovieGenres"), Trait("Action","GetAll")]
    public async Task GetAll_With_MovId_And_GenId_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieGenresReadController(db);

        var res = await sut.GetAll(movId: 999, genId: 888);
        Assert.IsType<NotFoundResult>(res.Result);
    }
    
}
