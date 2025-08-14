using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieGenres.Controllers; 
using cs_oppgave_05.Api.MovieGenres.Dtos;

namespace cs_oppgave_05.UnitTests.Controllers.MovieGenres;

public partial class MovieGenresWriteControllerTests : MovieGenresTestBase
{
    [Fact, Trait("Area","MovieGenres"), Trait("Action","DeleteById")]
    public async Task DeleteById_Removes_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "X", 1999);
        var g = SeedGenre(db, "Y");
        Link(db, m.MovId, g.GenId);

        var sut = new MovieGenresWriteController(db);
        var res = await sut.DeleteById(m.MovId, g.GenId);
        Assert.IsType<NoContentResult>(res);

        var still = await db.MovieGenres.FindAsync(new object[] { m.MovId, g.GenId });
        Assert.Null(still);
    }

    [Fact, Trait("Area","MovieGenres"), Trait("Action","DeleteById")]
    public async Task DeleteById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieGenresWriteController(db);
        var res = await sut.DeleteById(100, 200);
        Assert.IsType<NotFoundResult>(res);
    }

    [Fact, Trait("Area","MovieGenres"), Trait("Action","DeleteByBody")]
    public async Task DeleteByBody_Removes_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "B", 2000);
        var g = SeedGenre(db, "C");
        Link(db, m.MovId, g.GenId);

        var sut = new MovieGenresWriteController(db);
        var res = await sut.DeleteByBody(new MovieGenresDeleteDto { MovId = m.MovId, GenId = g.GenId });
        Assert.IsType<NoContentResult>(res);

        var still = await db.MovieGenres.FindAsync(new object[] { m.MovId, g.GenId });
        Assert.Null(still);
    }

    [Fact, Trait("Area","MovieGenres"), Trait("Action","DeleteByBody")]
    public async Task DeleteByBody_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieGenresWriteController(db);
        var res = await sut.DeleteByBody(new MovieGenresDeleteDto { MovId = 1, GenId = 2 });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
