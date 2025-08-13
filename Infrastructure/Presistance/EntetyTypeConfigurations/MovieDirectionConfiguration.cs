using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Entities;

public class MovieDirectionConfiguration : IEntityTypeConfiguration<MovieDirection>
{
    public void Configure(EntityTypeBuilder<MovieDirection> b)
    {
        b.ToTable("movie_direction");
        b.HasKey(md => new { md.DirId, md.MovId });

        b.HasOne(md => md.Director)
            .WithMany(d => d.MovieDirections)
            .HasForeignKey(md => md.DirId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(md => md.Movie)
            .WithMany(m => m.MovieDirections)
            .HasForeignKey(md => md.MovId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
