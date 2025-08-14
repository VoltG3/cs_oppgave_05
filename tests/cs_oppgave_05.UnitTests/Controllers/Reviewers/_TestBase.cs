using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Reviewers;

public abstract class ReviewersTestBase
{
    protected AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"ReviewersDb_{Guid.NewGuid()}")
            .Options;
        return new AppDbContext(opts);
    }

    protected Reviewer SeedReviewer(AppDbContext db, string name = "Seeded")
    {
        var r = new Reviewer { RevName = name };
        db.Reviewers.Add(r);
        db.SaveChanges();
        return r;
    }

    protected List<Reviewer> SeedReviewers(AppDbContext db, params string[] names)
    {
        var list = new List<Reviewer>();
        foreach (var n in names)
        {
            var r = new Reviewer { RevName = n };
            db.Reviewers.Add(r);
            list.Add(r);
        }
        db.SaveChanges();
        return list;
    }
    
}
