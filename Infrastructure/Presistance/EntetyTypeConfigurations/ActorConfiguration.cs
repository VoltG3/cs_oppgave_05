using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using cs_oppgave_05.Entities;

public class ActorConfiguration : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> b)
    {
        b.ToTable("actor");
        b.HasKey(a => a.ActId);

        b.Property(a => a.ActFname).IsRequired().HasMaxLength(100);
        b.Property(a => a.ActLname).IsRequired().HasMaxLength(100);
        b.Property(a => a.ActGender).HasMaxLength(10);
    }
}
