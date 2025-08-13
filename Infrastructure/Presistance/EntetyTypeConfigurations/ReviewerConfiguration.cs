using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Entities;

public class ReviewerConfiguration : IEntityTypeConfiguration<Reviewer>
{
    public void Configure(EntityTypeBuilder<Reviewer> b)
    {
        b.ToTable("reviewer");
        b.HasKey(r => r.RevId);

        b.Property(r => r.RevName)
            .IsRequired()
            .HasMaxLength(200);

        b.HasIndex(r => r.RevName);
    }
}
