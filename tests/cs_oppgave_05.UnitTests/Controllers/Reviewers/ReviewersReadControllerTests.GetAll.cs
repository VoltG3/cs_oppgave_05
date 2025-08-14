using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Reviewers.Controllers;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Reviewers;

public partial class ReviewersReadControllerTests : ReviewersTestBase
{
    [Fact, Trait("Area","Reviewers"), Trait("Action","GetAll")]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateDb();
        SeedReviewers(db, "A", "B", "C");

        var sut = new ReviewersReadController(db);

        var result = await sut.GetAll();
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<Reviewer>>(ok.Value);

        Assert.Equal(3, list.Count());
    }
    
}
