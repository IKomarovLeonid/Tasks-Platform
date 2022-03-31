using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Objects.Settings;

namespace Persistence.Configurations
{
    internal class SettingsDbConfiguration : IEntityTypeConfiguration<BaseSettings>
    {
        public void Configure(EntityTypeBuilder<BaseSettings> builder)
        {
            builder.HasKey(t => t.Key);
            builder.Property(t => t.Key).HasColumnName("key");

            builder.Property(t => t.Value).IsRequired().HasColumnName("value");
            builder.Property(t => t.UpdatedUtc).IsRequired().HasColumnName("updated_utc");
        }
    }
}
