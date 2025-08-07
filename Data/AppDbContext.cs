using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Models;

namespace cs_oppgave_05.Data;
    
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Director> Directors { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }
    public DbSet<MovieDirection> MovieDirections { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<MovieGenres> MovieGenress { get; set; }
    public DbSet<Genres> Genres { get; set; }
    
    // Foreign Key
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MovieDirection>()
            .HasKey(md => new { md.DirId, md.MovId });

        modelBuilder.Entity<MovieDirection>()
            .HasOne(md => md.Director)
            .WithMany(d => d.MovieDirections)
            .HasForeignKey(md => md.DirId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MovieDirection>()
            .HasOne(md => md.Movie)
            .WithMany(m => m.MovieDirections)
            .HasForeignKey(md => md.MovId)
            .OnDelete(DeleteBehavior.Cascade);

    }

}
