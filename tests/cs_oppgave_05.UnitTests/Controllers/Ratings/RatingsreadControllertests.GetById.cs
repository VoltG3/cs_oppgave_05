using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Ratings.Controllers;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Ratings;

public partial class RatingsReadControllerTests : RatingsTestBase
{
    [Fact, Trait("Area","Ratings"), Trait("Action","GetById")]
    public async Task GetById_Returns_Ok()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "X", 2005);
        var r = SeedReviewer(db, "Y");
        LinkRating(db, m.MovId, r.RevId, 4, 7);

        var sut = new RatingsReadController(db);
        var res = await sut.GetById(m.MovId, r.RevId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var rt = Assert.IsType<Rating>(ok.Value);

        Assert.Equal(m.MovId, rt.MovId);
        Assert.Equal(r.RevId, rt.RevId);
        Assert.Equal(4, rt.RevStars);
        Assert.Equal(7, rt.NumOfRatings);
    }

    [Fact, Trait("Area","Ratings"), Trait("Action","GetById")]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new RatingsReadController(db);
        var res = await sut.GetById(123, 456);
        Assert.IsType<NotFoundResult>(res.Result);
    }
    
}
