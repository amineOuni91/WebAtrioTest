using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAtrioTest.Models;

namespace WebAtrioTest.Infrastructure.Configuration;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Organisation)
               .IsRequired();

        builder.Property(x => x.Position)
                .IsRequired();

        builder.Property(x => x.StartDate)
               .IsRequired();

        builder.Property(x => x.EndDate)
               .IsRequired(false);

        builder.HasOne(x => x.Person)
               .WithMany(x => x.Jobs)
               .HasForeignKey(x => x.PersonId)
               .IsRequired();
    }
}
