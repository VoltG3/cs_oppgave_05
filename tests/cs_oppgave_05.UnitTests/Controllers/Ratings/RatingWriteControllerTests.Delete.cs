using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Ratings.Controllers; 
using cs_oppgave_05.Api.Ratings.Dtos; 

namespace cs_oppgave_05.UnitTests.Controllers.Ratings;

public partial class RatingsWriteControllerTests : RatingsTestBase
{
    [Fact, Trait("Area","Ratings"), Trait("Action","DeleteById")]
    public async Task DeleteById_Removes_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "R", 2003);
        var r = SeedReviewer(db, "Z");
        LinkRating(db, m.MovId, r.RevId, 5, 9);

        var sut = new RatingsWriteController(db);
        var res = await sut.DeleteById(m.MovId, r.RevId);
        Assert.IsType<NoContentResult>(res);

        var gone = await db.Ratings.FindAsync(m.MovId, r.RevId);
        Assert.Null(gone);
    }

    [Fact, Trait("Area","Ratings"), Trait("Action","DeleteById")]
    public async Task DeleteById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new RatingsWriteController(db);
        var res = await sut.DeleteById(999, 888);
        Assert.IsType<NotFoundResult>(res);
    }

    [Fact, Trait("Area","Ratings"), Trait("Action","DeleteByBody")]
    public async Task DeleteByBody_Removes_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "B", 2000);
        var r = SeedReviewer(db, "C");
        LinkRating(db, m.MovId, r.RevId, 1, 1);

        var sut = new RatingsWriteController(db);
        var res = await sut.DeleteByBody(new RatingDeleteDto { MovId = m.MovId, RevId = r.RevId });
        Assert.IsType<NoContentResult>(res);

        var still = await db.Ratings.FindAsync(m.MovId, r.RevId);
        Assert.Null(still);
    }

    [Fact, Trait("Area","Ratings"), Trait("Action","DeleteByBody")]
    public async Task DeleteByBody_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new RatingsWriteController(db);
        var res = await sut.DeleteByBody(new RatingDeleteDto { MovId = 1, RevId = 2 });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
