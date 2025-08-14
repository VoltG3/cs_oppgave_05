using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Ratings.Controllers; 
using cs_oppgave_05.Api.Ratings.Dtos; 

namespace cs_oppgave_05.UnitTests.Controllers.Ratings;

public partial class RatingsWriteControllerTests : RatingsTestBase
{
    [Fact, Trait("Area","Ratings"), Trait("Action","Patch")]
    public async Task Patch_Updates_Stars_And_Count_When_Provided()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "X", 2010);
        var r = SeedReviewer(db, "Y");
        LinkRating(db, m.MovId, r.RevId, 2, 1);

        var sut = new RatingsWriteController(db);

        var res = await sut.Patch(m.MovId, r.RevId, new UpdateRatingDto { RevStars = 4, NumOfRatings = 5 });
        Assert.IsType<NoContentResult>(res);

        var fromDb = await db.Ratings.FindAsync(m.MovId, r.RevId);
        Assert.NotNull(fromDb);
        Assert.Equal(4, fromDb!.RevStars);
        Assert.Equal(5, fromDb.NumOfRatings);
    }

    [Fact, Trait("Area","Ratings"), Trait("Action","Patch")]
    public async Task Patch_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new RatingsWriteController(db);

        var res = await sut.Patch(111, 222, new UpdateRatingDto { RevStars = 3 });
        Assert.IsType<NotFoundResult>(res);
    }
    
}
