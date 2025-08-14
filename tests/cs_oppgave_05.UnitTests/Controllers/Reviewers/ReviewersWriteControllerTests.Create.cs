using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Reviewers.Controllers;
using cs_oppgave_05.Api.Reviewers.Dtos;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Reviewers;

public partial class ReviewersWriteControllerTests : ReviewersTestBase
{
    [Fact, Trait("Area","Reviewers"), Trait("Action","Create")]
    public async Task Create_Returns_CreatedAtRoute_And_Persists()
    {
        using var db = CreateDb();
        var sut = new ReviewersWriteController(db);

        var dto = new CreateReviewerDto { RevName = "Test Reviewer" };

        var post = await sut.Create(dto);
        var created = Assert.IsType<CreatedAtRouteResult>(post.Result);
        Assert.Equal("GetReviewerById", created.RouteName);

        var entity = Assert.IsType<Reviewer>(created.Value);
        Assert.True(entity.RevId > 0);
        Assert.Equal("Test Reviewer", entity.RevName);

        // verify in DB
        var fromDb = await db.Reviewers.FindAsync(entity.RevId);
        Assert.NotNull(fromDb);
        Assert.Equal("Test Reviewer", fromDb!.RevName);
    }
    
}
