using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Actors.Controllers; 
using cs_oppgave_05.Api.Actors.Dtos; 
using cs_oppgave_05.Entities; 

namespace cs_oppgave_05.UnitTests.Controllers.Actors;

public partial class ActorsWriteControllerTests : ActorsTestBase
{
    [Fact, Trait("Area","Actors"), Trait("Action","Create")]
    public async Task Create_Returns_CreatedAtRoute_And_Persists()
    {
        using var db = CreateDb();
        var sut = new ActorsWriteController(db);

        var dto = new CreateActorDto { ActFname = "Greta", ActLname = "Gerwig", ActGender = "F" };

        var post = await sut.Create(dto);
        var created = Assert.IsType<CreatedAtRouteResult>(post.Result);
        Assert.Equal("GetActorById", created.RouteName);

        var entity = Assert.IsType<Actor>(created.Value);
        Assert.True(entity.ActId > 0);
        Assert.Equal("Greta", entity.ActFname);
        Assert.Equal("Gerwig", entity.ActLname);
        Assert.Equal("F", entity.ActGender);

        var fromDb = await db.Actors.FindAsync(entity.ActId);
        Assert.NotNull(fromDb);
        Assert.Equal("Greta", fromDb!.ActFname);
    }
    
}
