using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Ratings;

public abstract class RatingsTestBase
{
    protected AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"RatingsDb_{Guid.NewGuid()}")
            .Options;
        return new AppDbContext(opts);
    }

    protected Movie SeedMovie(AppDbContext db, string title = "Seeded", int year = 2000)
    {
        var m = new Movie { MovTitle = title, MovYear = year };
        db.Movies.Add(m);
        db.SaveChanges();
        return m;
    }

    protected Reviewer SeedReviewer(AppDbContext db, string name = "Critic")
    {
        var r = new Reviewer { RevName = name };
        db.Reviewers.Add(r);
        db.SaveChanges();
        return r;
    }

    protected Rating LinkRating(AppDbContext db, int movId, int revId, int stars = 4, int num = 1)
    {
        var rt = new Rating { MovId = movId, RevId = revId, RevStars = stars, NumOfRatings = num };
        db.Ratings.Add(rt);
        db.SaveChanges();
        return rt;
    }
    
}
