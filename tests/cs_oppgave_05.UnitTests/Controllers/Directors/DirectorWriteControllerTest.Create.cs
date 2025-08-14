using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Directors.Controllers; 
using cs_oppgave_05.Api.Directors.Dtos; 
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Directors;

public partial class DirectorsWriteControllerTests : DirectorsTestBase
{
    [Fact, Trait("Area","Directors"), Trait("Action","Create")]
    public async Task Create_Returns_CreatedAtRoute_And_Persists()
    {
        using var db = CreateDb();
        var sut = new DirectorsWriteController(db);

        var dto = new CreateDirectorDto { DirFname = "Greta", DirLname = "Gerwig" };

        var post = await sut.Create(dto); // ja action nosaukums cits – pielāgo
        var created = Assert.IsType<CreatedAtRouteResult>(post.Result);

        // sagaidām, ka lasītājs ir ar Name="GetDirectorById"
        Assert.Equal("GetDirectorById", created.RouteName);

        var entity = Assert.IsType<Director>(created.Value);
        Assert.True(entity.DirId > 0);
        Assert.Equal("Greta", entity.DirFname);
        Assert.Equal("Gerwig", entity.DirLname);

        // verify DB
        var fromDb = await db.Directors.FindAsync(entity.DirId);
        Assert.NotNull(fromDb);
        Assert.Equal("Greta", fromDb!.DirFname);
        Assert.Equal("Gerwig", fromDb!.DirLname);
    }
    
}
