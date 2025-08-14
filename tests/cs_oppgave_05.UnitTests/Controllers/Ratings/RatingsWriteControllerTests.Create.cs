using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Ratings.Controllers; 
using cs_oppgave_05.Api.Ratings.Dtos; 
using cs_oppgave_05.Entities;  
namespace cs_oppgave_05.UnitTests.Controllers.Ratings;

public partial class RatingsWriteControllerTests : RatingsTestBase
{
    [Fact, Trait("Area","Ratings"), Trait("Action","Create")]
    public async Task Create_Returns_CreatedAtRoute_When_Movie_And_Reviewer_Exist()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "New", 2024);
        var r = SeedReviewer(db, "Critic");
        var sut = new RatingsWriteController(db);

        var dto = new CreateRatingDto { MovId = m.MovId, RevId = r.RevId, RevStars = 5, NumOfRatings = 2 };
        var res = await sut.Create(dto);
        var created = Assert.IsType<CreatedAtRouteResult>(res.Result);

        Assert.Equal("GetRatingById", created.RouteName);
        var entity = Assert.IsType<Rating>(created.Value);
        Assert.Equal(m.MovId, entity.MovId);
        Assert.Equal(r.RevId, entity.RevId);
        Assert.Equal(5, entity.RevStars);
        Assert.Equal(2, entity.NumOfRatings);
    }

    [Fact, Trait("Area","Ratings"), Trait("Action","Create")]
    public async Task Create_Returns_BadRequest_When_Movie_Or_Reviewer_Missing()
    {
        using var db = CreateDb();
        var sut = new RatingsWriteController(db);

        // Movie missing, Reviewer exists
        var rev = SeedReviewer(db, "OnlyReviewer");
        var bad1 = await sut.Create(new CreateRatingDto { MovId = 999, RevId = rev.RevId, RevStars = 3, NumOfRatings = 1 });
        Assert.IsType<BadRequestObjectResult>(bad1.Result);

        // Reviewer missing, Movie exists
        var mov = SeedMovie(db, "OnlyMovie", 2001);
        var bad2 = await sut.Create(new CreateRatingDto { MovId = mov.MovId, RevId = 999, RevStars = 4, NumOfRatings = 2 });
        Assert.IsType<BadRequestObjectResult>(bad2.Result);
    }

    [Fact, Trait("Area","Ratings"), Trait("Action","Create")]
    public async Task Create_Returns_Conflict_When_Duplicate_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "Dup", 2000);
        var r = SeedReviewer(db, "DupRev");
        LinkRating(db, m.MovId, r.RevId, 2, 1);

        var sut = new RatingsWriteController(db);
        var res = await sut.Create(new CreateRatingDto { MovId = m.MovId, RevId = r.RevId, RevStars = 2, NumOfRatings = 1 });

        Assert.IsType<ConflictObjectResult>(res.Result);
    }
    
}
