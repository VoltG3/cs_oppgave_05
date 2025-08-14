using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Directors.Controllers;
using cs_oppgave_05.Entities;            

namespace cs_oppgave_05.UnitTests.Controllers.Directors;

public partial class DirectorsReadControllerTests : DirectorsTestBase
{
    [Fact, Trait("Area","Directors"), Trait("Action","GetAll")]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateDb();
        SeedDirectors(db, ("A","One"), ("B","Two"), ("C","Three"));

        var sut = new DirectorsReadController(db);

        var result = await sut.GetAll();
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<Director>>(ok.Value);

        Assert.Equal(3, list.Count());
    }
    
}
