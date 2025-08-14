using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieDirections.Controllers; 
using cs_oppgave_05.Api.MovieDirections.Dtos.Contracts; 

namespace cs_oppgave_05.UnitTests.Controllers.MovieDirection;

public partial class MovieDirectionWriteControllerTests : MovieDirectionTestBase
{
    [Fact, Trait("Area","MovieDirection"), Trait("Action","DeleteById")]
    public async Task DeleteById_Removes_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "R", 2003);
        var d = SeedDirector(db, "C", "D");
        Link(db, m.MovId, d.DirId);

        var sut = new MovieDirectionsWriteController(db);
        var res = await sut.DeleteById(m.MovId, d.DirId);
        Assert.IsType<NoContentResult>(res);

        var gone = await db.MovieDirections.FindAsync(m.MovId, d.DirId);
        Assert.Null(gone);
    }

    [Fact, Trait("Area","MovieDirection"), Trait("Action","DeleteById")]
    public async Task DeleteById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieDirectionsWriteController(db);
        var res = await sut.DeleteById(999, 888);
        Assert.IsType<NotFoundResult>(res);
    }

    [Fact, Trait("Area","MovieDirection"), Trait("Action","DeleteByBody")]
    public async Task DeleteByBody_Removes_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "B", 2000);
        var d = SeedDirector(db, "E", "F");
        Link(db, m.MovId, d.DirId);

        var sut = new MovieDirectionsWriteController(db);
        var res = await sut.DeleteByBody(new MovieDirectionDeleteDto { MovId = m.MovId, DirId = d.DirId });
        Assert.IsType<NoContentResult>(res);

        var still = await db.MovieDirections.FindAsync(m.MovId, d.DirId);
        Assert.Null(still);
    }

    [Fact, Trait("Area","MovieDirection"), Trait("Action","DeleteByBody")]
    public async Task DeleteByBody_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieDirectionsWriteController(db);
        var res = await sut.DeleteByBody(new MovieDirectionDeleteDto { MovId = 1, DirId = 2 });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
