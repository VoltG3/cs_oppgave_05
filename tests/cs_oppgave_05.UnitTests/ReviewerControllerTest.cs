using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using cs_oppgave_05.Infrastructure.Presistance;           
using cs_oppgave_05.Api.Reviewers.Controllers;            
using cs_oppgave_05.Api.Reviewers.Dtos;                   
using cs_oppgave_05.Entities;                            

public class ReviewerControllerTests
{
    private static AppDbContext CreateInMemoryDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"TestsDb_{System.Guid.NewGuid()}")
            .Options;
        return new AppDbContext(opts);
    }

    [Fact]
    public async Task Create_Then_GetById_Works()
    {
        using var db = CreateInMemoryDb();

        // write controller (POST)
        var write = new ReviewersWriteController(db);
        var dto = new CreateReviewerDto { RevName = "Test Reviewer" };

        var post = await write.Create(dto);
        var created = Assert.IsType<CreatedAtRouteResult>(post.Result);
        Assert.Equal("GetReviewerById", created.RouteName);

        var createdReviewer = Assert.IsType<Reviewer>(created.Value);
        Assert.True(createdReviewer.RevId > 0);
        Assert.Equal("Test Reviewer", createdReviewer.RevName);

        // read controller (GET by id)
        var read = new ReviewersReadController(db);
        var get = await read.GetById(createdReviewer.RevId);
        var ok = Assert.IsType<OkObjectResult>(get.Result);
        var returned = Assert.IsType<Reviewer>(ok.Value);

        Assert.Equal(createdReviewer.RevId, returned.RevId);
        Assert.Equal("Test Reviewer", returned.RevName);
    }

    [Fact]
    public async Task GetAll_Returns_List()
    {
        using var db = CreateInMemoryDb();
        db.Reviewers.Add(new Reviewer { RevName = "A" });
        db.Reviewers.Add(new Reviewer { RevName = "B" });
        await db.SaveChangesAsync();

        var read = new ReviewersReadController(db);
        var result = await read.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<Reviewer>>(ok.Value);
        // pēc vajadzības: Assert.Equal(2, list.Count());
    }
}
