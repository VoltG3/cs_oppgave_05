using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Actors.Controllers; 
using cs_oppgave_05.Entities; 

namespace cs_oppgave_05.UnitTests.Controllers.Actors;

public partial class ActorsReadControllerTests : ActorsTestBase
{
    [Fact, Trait("Area","Actors"), Trait("Action","GetAll")]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateDb();
        SeedActors(db, ("A","One","F"), ("B","Two","M"), ("C","Three","F"));

        var sut = new ActorReadController(db);

        var result = await sut.GetAll();
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<Actor>>(ok.Value);

        Assert.Equal(3, list.Count());
    }
    
}
