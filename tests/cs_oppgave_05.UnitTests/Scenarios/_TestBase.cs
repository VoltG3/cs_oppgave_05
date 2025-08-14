using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Infrastructure.Presistance;
using cs_oppgave_05.Entities;

namespace cs_oppgave_05.UnitTests.Scenarios.MovieDetails;

public abstract class MovieDetailsTestBase
{
    protected AppDbContext CreateRelationalDb()
    {
        var conn = new SqliteConnection("Data Source=:memory:");
        conn.Open();

        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(conn)
            .Options;

        var db = new AppDbContext(opts);
        db.Database.EnsureCreated();
        return db;
    }

    protected (Genres genre, Director director, Actor actor, Reviewer reviewer) SeedMaster(AppDbContext db)
    {
        var genre    = new Genres   { GenTitle = "Documentar NSDAP" };
        var director = new Director { DirFname = "Leni",    DirLname = "fon Riefenstahl" };
        var actor    = new Actor    { ActFname = "Manfred", ActLname = "fon Richthofen", ActGender = "M" };
        var reviewer = new Reviewer { RevName  = "Die Deutsche Wochenschau" };
        db.Genres.Add(genre);
        db.Directors.Add(director);
        db.Actors.Add(actor);
        db.Reviewers.Add(reviewer);
        db.SaveChanges();
        return (genre, director, actor, reviewer);
    }
    
}
