using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Controllers.Actors;

public abstract class ActorsTestBase
{
    protected AppDbContext CreateDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"ActorsDb_{Guid.NewGuid()}")
            .Options;
        return new AppDbContext(opts);
    }

    protected Actor SeedActor(AppDbContext db, string fname = "John", string lname = "Doe", string gender = "M")
    {
        var a = new Actor { ActFname = fname, ActLname = lname, ActGender = gender };
        db.Actors.Add(a);
        db.SaveChanges();
        return a;
    }

    protected List<Actor> SeedActors(AppDbContext db, params (string F, string L, string G)[] people)
    {
        var list = new List<Actor>();
        foreach (var (f, l, g) in people)
        {
            var a = new Actor { ActFname = f, ActLname = l, ActGender = g };
            db.Actors.Add(a);
            list.Add(a);
        }
        db.SaveChanges();
        return list;
    }
    
}
