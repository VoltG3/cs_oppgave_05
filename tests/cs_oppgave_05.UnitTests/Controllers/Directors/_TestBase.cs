using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Directors;

public abstract class DirectorsTestBase
{
    protected AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"DirectorsDb_{Guid.NewGuid()}")
            .Options;
        return new AppDbContext(opts);
    }

    protected Director SeedDirector(AppDbContext db, string fname = "John", string lname = "Doe")
    {
        var d = new Director { DirFname = fname, DirLname = lname };
        db.Directors.Add(d);
        db.SaveChanges();
        return d;
    }

    protected List<Director> SeedDirectors(AppDbContext db, params (string F, string L)[] names)
    {
        var list = new List<Director>();
        foreach (var (f, l) in names)
        {
            var d = new Director { DirFname = f, DirLname = l };
            db.Directors.Add(d);
            list.Add(d);
        }
        db.SaveChanges();
        return list;
    }
    
}
