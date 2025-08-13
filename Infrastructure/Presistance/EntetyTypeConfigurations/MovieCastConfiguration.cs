using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Entities;

public class MovieCastConfiguration : IEntityTypeConfiguration<MovieCast>
{
    public void Configure(EntityTypeBuilder<MovieCast> b)
    {
        b.ToTable("movie_cast");
        b.HasKey(mc => new { mc.ActId, mc.MovId });

        b.Property(mc => mc.Role).HasMaxLength(150);

        b.HasOne(mc => mc.Actor)
            .WithMany(a => a.MovieCasts)
            .HasForeignKey(mc => mc.ActId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(mc => mc.Movie)
            .WithMany(m => m.MovieCasts)
            .HasForeignKey(mc => mc.MovId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
