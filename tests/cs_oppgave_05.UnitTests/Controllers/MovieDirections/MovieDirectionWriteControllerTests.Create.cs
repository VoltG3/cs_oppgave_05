using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Api.MovieDirections.Controllers; 
using cs_oppgave_05.Api.MovieDirections.Dtos;

namespace cs_oppgave_05.UnitTests.Controllers.MovieDirection;

public partial class MovieDirectionWriteControllerTests : MovieDirectionTestBase
{
    [Fact, Trait("Area","MovieDirection"), Trait("Action","Create")]
    public async Task Create_Returns_CreatedAtRoute_When_Movie_And_Director_Exist()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "New", 2024);
        var d = SeedDirector(db, "Greta", "Gerwig");
        var sut = new MovieDirectionsWriteController(db);

        var dto = new CreateMovieDirectionDto { MovId = m.MovId, DirId = d.DirId };
        var res = await sut.Create(dto);
        var created = Assert.IsType<CreatedAtRouteResult>(res.Result);

        Assert.Equal("GetMovieDirectionById", created.RouteName);
        var entity = Assert.IsType<Entities.MovieDirection>(created.Value);
        Assert.Equal(m.MovId, entity.MovId);
        Assert.Equal(d.DirId, entity.DirId);
    }

    [Fact, Trait("Area","MovieDirection"), Trait("Action","Create")]
    public async Task Create_Returns_BadRequest_When_Movie_Or_Director_Missing()
    {
        using var db = CreateDb();
        var sut = new MovieDirectionsWriteController(db);

        // Movie missing, Director exists
        var d = SeedDirector(db, "Only", "Dir");
        var bad1 = await sut.Create(new CreateMovieDirectionDto { MovId = 999, DirId = d.DirId });
        Assert.IsType<BadRequestObjectResult>(bad1.Result);

        // Director missing, Movie exists
        var m = SeedMovie(db, "OnlyMovie", 2001);
        var bad2 = await sut.Create(new CreateMovieDirectionDto { MovId = m.MovId, DirId = 999 });
        Assert.IsType<BadRequestObjectResult>(bad2.Result);
    }

    [Fact, Trait("Area","MovieDirection"), Trait("Action","Create")]
    public async Task Create_Returns_Conflict_When_Duplicate_Link()
    {
        using var db = CreateDb();
        var m = SeedMovie(db, "Dup", 2000);
        var d = SeedDirector(db, "Dup", "Dir");
        Link(db, m.MovId, d.DirId);

        var sut = new MovieDirectionsWriteController(db);
        var res = await sut.Create(new CreateMovieDirectionDto { MovId = m.MovId, DirId = d.DirId });

        Assert.IsType<ConflictObjectResult>(res.Result);
    }
    
}
