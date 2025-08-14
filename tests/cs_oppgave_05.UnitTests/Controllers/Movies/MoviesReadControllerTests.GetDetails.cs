using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.Movies.Controllers; 
using cs_oppgave_05.Api.Movies.Dtos; 

namespace cs_oppgave_05.UnitTests.Controllers.Movies;

public partial class MoviesReadControllerTests : MoviesTestBase
{
    [Fact, Trait("Area","Movies"), Trait("Action","GetDetails")]
    public async Task GetDetails_Default_Includes_Returns_Ok_With_Empty_Collections_When_No_Relations()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "Solo", 2020);
        var sut = new MoviesReadController(db); 

        var res = await sut.GetMovieDetails(m.MovId, include: null);
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var dto = Assert.IsType<MovieDetailsDto>(ok.Value);

        Assert.Equal(m.MovId, dto.MovId);
        Assert.Equal("Solo", dto.MovTitle);
    
        Assert.Empty(dto.Genres);
        Assert.Empty(dto.Directors);
        Assert.Empty(dto.Cast);
        Assert.Empty(dto.Ratings);
    }

    [Fact, Trait("Area","Movies"), Trait("Action","GetDetails")]
    public async Task GetDetails_With_Filter_Only_Genres_And_Cast()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "HasRels", 2021);
        
        var sut = new MoviesReadController(db);

        var res = await sut.GetMovieDetails(m.MovId, include: "genres,cast");
        var ok = Assert.IsType<OkObjectResult>(res.Result);
        var dto = Assert.IsType<MovieDetailsDto>(ok.Value);
        
        Assert.Empty(dto.Genres);
        Assert.Empty(dto.Cast);
        Assert.Empty(dto.Directors);
        Assert.Empty(dto.Ratings);
    }
    
}
