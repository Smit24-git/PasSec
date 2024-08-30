using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasSecWebApi.Persistence.Configurations
{
    public class VaultStorageKeyCondiguration : IEntityTypeConfiguration<VaultStorageKey>
    {
        public void Configure(EntityTypeBuilder<VaultStorageKey> builder)
        {
            builder.HasKey(x => x.VaultStorageKeyId);

            builder.Property(e => e.KeyName)
                .IsRequired();

            builder.Property(e => e.VaultId)
                .IsRequired();
            builder.Property(e => e.Password)
                .IsRequired();
            builder.Property(e => e.IV)
                .IsRequired();
            builder.Property(e => e.AddedBy)
                .IsRequired();
        }
    }
}
