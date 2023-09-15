using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ouijjane.Village.Domain.Entities;

namespace Ouijjane.Village.Infrastructure.Persistence.Configurations;
public class InhabitantConfiguration : IEntityTypeConfiguration<Inhabitant>
{
    public void Configure(EntityTypeBuilder<Inhabitant> builder)
    {
        builder.ToTable(nameof(Inhabitant));

        builder.Property(x => x.FirstName)
               .HasColumnType("nvarchar(100)")
               .IsRequired();

        builder.Property(x => x.LastName)
               .HasColumnType("nvarchar(100)")
               .IsRequired();

        builder.Property(x => x.FatherName)
               .HasColumnType("nvarchar(100)")
               .IsRequired();

        builder.Property(x => x.IsMarried)
               .HasDefaultValue(false)
               .IsRequired();

    }
}
