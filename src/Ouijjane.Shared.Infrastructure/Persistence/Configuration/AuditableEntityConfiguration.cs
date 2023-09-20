using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ouijjane.Shared.Domain.Interfaces;

namespace Ouijjane.Shared.Infrastructure.Persistence.Configuration;
public abstract class AuditTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.CreatedDateTime)
               .HasColumnType("timestamp")
               .IsRequired();

        builder.Property(e => e.ModifiedDateTime)
               .HasColumnType("timestamp")
               .IsRequired(false);

        builder.Property(e => e.CreatedByUserId)
               .HasColumnType("int")
               .IsRequired(false);

        builder.Property(e => e.ModifiedByUserId)
               .HasColumnType("int")
               .IsRequired(false);
    }
}