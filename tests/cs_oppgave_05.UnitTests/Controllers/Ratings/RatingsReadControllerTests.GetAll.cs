using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Ratings.Controllers;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Ratings;

public partial class RatingsReadControllerTests : RatingsTestBase
{
    [Fact, Trait("Area","Ratings"), Trait("Action","GetAll")]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateDb();
        var m1 = SeedMovie(db, "A", 1999);
        var m2 = SeedMovie(db, "B", 2001);
        var r1 = SeedReviewer(db, "Alpha");
        var r2 = SeedReviewer(db, "Beta");

        LinkRating(db, m1.MovId, r1.RevId, 5, 3);
        LinkRating(db, m2.MovId, r2.RevId, 2, 1);

        var sut = new RatingsReadController(db);

        var res = await sut.GetAll(movId: null, revId: null);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<Rating>>(ok.Value);
        Assert.Equal(2, list.Count());
    }

    [Fact, Trait("Area","Ratings"), Trait("Action","GetAll")]
    public async Task GetAll_With_MovId_And_RevId_Returns_Single()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "One", 2024);
        var r = SeedReviewer(db, "Solo");
        LinkRating(db, m.MovId, r.RevId, 3, 2);

        var sut = new RatingsReadController(db);

        var res = await sut.GetAll(movId: m.MovId, revId: r.RevId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var rating = Assert.IsType<Rating>(ok.Value);
        Assert.Equal(m.MovId, rating.MovId);
        Assert.Equal(r.RevId, rating.RevId);
        Assert.Equal(3, rating.RevStars);
        Assert.Equal(2, rating.NumOfRatings);
    }

    [Fact, Trait("Area","Ratings"), Trait("Action","GetAll")]
    public async Task GetAll_With_MovId_And_RevId_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new RatingsReadController(db);

        var res = await sut.GetAll(movId: 999, revId: 888);
        Assert.IsType<NotFoundResult>(res.Result);
    }
    
}
