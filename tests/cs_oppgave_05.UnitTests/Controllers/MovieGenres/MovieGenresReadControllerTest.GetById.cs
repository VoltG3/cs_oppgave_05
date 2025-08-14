using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieGenres.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.MovieGenres;

public partial class MovieGenresReadControllerTests : MovieGenresTestBase
{
    [Fact, Trait("Area","MovieGenres"), Trait("Action","GetById")]
    public async Task GetById_Returns_Ok()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "X", 2005);
        var g = SeedGenre(db, "Thriller");
        Link(db, m.MovId, g.GenId);

        var sut = new MovieGenresReadController(db);
        var res = await sut.GetById(m.MovId, g.GenId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var mg = Assert.IsType<Entities.MovieGenres>(ok.Value);

        Assert.Equal(m.MovId, mg.MovId);
        Assert.Equal(g.GenId, mg.GenId);
    }

    [Fact, Trait("Area","MovieGenres"), Trait("Action","GetById")]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieGenresReadController(db);
        var res = await sut.GetById(123, 456);
        Assert.IsType<NotFoundResult>(res.Result);
    }
    
}
