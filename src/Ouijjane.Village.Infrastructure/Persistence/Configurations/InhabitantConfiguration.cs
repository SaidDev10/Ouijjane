using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ouijjane.Shared.Infrastructure.Persistence.Configuration;
using Ouijjane.Village.Domain.Entities;

namespace Ouijjane.Village.Infrastructure.Persistence.Configurations;
public class InhabitantConfiguration : AuditTypeConfiguration<Inhabitant>
{
    public override void Configure(EntityTypeBuilder<Inhabitant> builder)
    {
        builder.ToTable(nameof(Inhabitant), SchemaNames.Dbo);

        base.Configure(builder);

        builder.Property(x => x.FirstName)
               .HasColumnType("varchar(100)")
               .IsRequired();

        builder.Property(x => x.LastName)
               .HasColumnType("varchar(100)")
               .IsRequired();

        builder.Property(x => x.FatherName)
               .HasColumnType("varchar(100)")
               .IsRequired();

        builder.Property(x => x.IsMarried)
               .HasDefaultValue(false)
               .IsRequired();

        builder.HasIndex(x => new { x.LastName, x.FirstName });

    }
}
