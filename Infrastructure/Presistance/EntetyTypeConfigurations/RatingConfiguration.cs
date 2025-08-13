using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Entities;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> b)
    {
        b.ToTable("Ratings");
        b.HasKey(r => new { r.MovId, r.RevId });
        
        b.Property(r => r.RevStars)
            .HasColumnType("decimal(3,1)");

        b.HasOne(r => r.Movie)
            .WithMany(m => m.Ratings)
            .HasForeignKey(r => r.MovId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(r => r.Reviewer)
            .WithMany(rv => rv.Ratings)
            .HasForeignKey(r => r.RevId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
