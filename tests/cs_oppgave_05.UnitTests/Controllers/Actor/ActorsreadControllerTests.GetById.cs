using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Actors.Controllers;
using cs_oppgave_05.Entities; 

namespace cs_oppgave_05.UnitTests.Controllers.Actors;

public partial class ActorsReadControllerTests : ActorsTestBase
{
    [Fact, Trait("Area","Actors"), Trait("Action","GetById")]
    public async Task GetById_Returns_NotFound_When_Missing()
    {
        using var db = CreateDb();
        var sut = new ActorReadController(db);

        var res = await sut.GetById(999);
        Assert.IsType<NotFoundResult>(res.Result);
    }

    [Fact, Trait("Area","Actors"), Trait("Action","GetById")]
    public async Task GetById_Returns_Ok_For_Existing()
    {
        using var db = CreateDb();
        var seeded = SeedActor(db, "Chris", "Nolan", "M");
        var sut = new ActorReadController(db);

        var res = await sut.GetById(seeded.ActId);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var dto = Assert.IsType<Actor>(ok.Value);

        Assert.Equal(seeded.ActId, dto.ActId);
        Assert.Equal("Chris", dto.ActFname);
        Assert.Equal("Nolan", dto.ActLname);
        Assert.Equal("M", dto.ActGender);
    }
    
}
