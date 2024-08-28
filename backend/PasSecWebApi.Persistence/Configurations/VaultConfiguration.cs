using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasSecWebApi.Persistence.Configurations
{
    internal class VaultConfiguration : IEntityTypeConfiguration<Vault>
    {
        public void Configure(EntityTypeBuilder<Vault> builder)
        {
            builder.HasKey(e => e.VaultId);

            builder.Property(e => e.VaultName)
                .IsRequired();
            builder.Property(e => e.AppliedCustomKey)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(e => e.IV)
                .IsRequired();
            builder.Property(e => e.AddedBy)
                .IsRequired();
        }
    }
}
