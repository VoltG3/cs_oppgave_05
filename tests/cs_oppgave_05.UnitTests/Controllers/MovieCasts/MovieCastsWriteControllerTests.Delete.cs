using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieCasts.Controllers; 
using cs_oppgave_05.Api.MovieCasts.Dtos;  

namespace cs_oppgave_05.UnitTests.Controllers.MovieCasts;

public partial class MovieCastsWriteControllerTests : MovieCastsTestBase
{
    [Fact, Trait("Area","MovieCasts"), Trait("Action","DeleteById")]
    public async Task DeleteById_Removes_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "R", 2003);
        var a = SeedActor(db, "C", "D", "F");
        Link(db, m.MovId, a.ActId, "Role");

        var sut = new MovieCastsWriteController(db);
        var res = await sut.DeleteById(m.MovId, a.ActId);
        Assert.IsType<NoContentResult>(res);

        var gone = await db.MovieCasts.FindAsync(m.MovId, a.ActId);
        Assert.Null(gone);
    }

    [Fact, Trait("Area","MovieCasts"), Trait("Action","DeleteById")]
    public async Task DeleteById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieCastsWriteController(db);
        var res = await sut.DeleteById(999, 888);
        Assert.IsType<NotFoundResult>(res);
    }

    [Fact, Trait("Area","MovieCasts"), Trait("Action","DeleteByBody")]
    public async Task DeleteByBody_Removes_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "B", 2000);
        var a = SeedActor(db, "E", "F", "M");
        Link(db, m.MovId, a.ActId, "Side");

        var sut = new MovieCastsWriteController(db);
        var res = await sut.DeleteByBody(new MovieCastDeleteDto { MovId = m.MovId, ActId = a.ActId });
        Assert.IsType<NoContentResult>(res);

        var still = await db.MovieCasts.FindAsync(m.MovId, a.ActId);
        Assert.Null(still);
    }

    [Fact, Trait("Area","MovieCasts"), Trait("Action","DeleteByBody")]
    public async Task DeleteByBody_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieCastsWriteController(db);
        var res = await sut.DeleteByBody(new MovieCastDeleteDto { MovId = 1, ActId = 2 });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
