using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Genres.Controllers; 

namespace cs_oppgave_05.UnitTests.Controllers.Genres;

public partial class GenresReadControllerTests : GenresTestBase
{
    [Fact, Trait("Area","Genres"), Trait("Action","GetAll")]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateDb();
        SeedGenres(db, "Drama", "Comedy", "Sci-Fi");

        var sut = new GenresReadController(db);
        
        var result = await sut.GetAll();
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<Entities.Genres>>(ok.Value);
        
        Assert.Equal(3, list.Count());
    }
    
}
