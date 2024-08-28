using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasSecWebApi.Persistence.Configurations
{
    public class VaultStorageKeySecurityQAConfiguration : IEntityTypeConfiguration<VaultStorageKeySecurityQA>
    {
        public void Configure(EntityTypeBuilder<VaultStorageKeySecurityQA> builder)
        {
            builder.HasKey(e => e.VaultStorageKeySecurityQAId);

            builder.Property(e => e.Question)
                .IsRequired();
            builder.Property(e => e.Answer)
                .IsRequired();
            builder.Property(e => e.AddedBy)
                .IsRequired();
            builder.Property(e => e.IV)
                .IsRequired();
            builder.Property(e => e.VaultStorageKeyId)
                .IsRequired();

        }
    }
}
