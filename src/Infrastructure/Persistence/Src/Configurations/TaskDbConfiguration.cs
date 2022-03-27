using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Objects.Common;
using Objects.Dto;

namespace Persistence.Configurations
{
    internal class TaskDbConfiguration : IEntityTypeConfiguration<TaskDto>
    {
        public void Configure(EntityTypeBuilder<TaskDto> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id");

            builder.Property(t => t.Title).IsRequired().HasColumnName("title");
            builder.Property(t => t.Description).IsRequired().HasColumnName("description");
            builder.Property(t => t.State).IsRequired().HasColumnName("state").HasConversion(new EnumToStringConverter<RootState>());
            builder.Property(t => t.Status).IsRequired().HasColumnName("status").HasConversion(new EnumToStringConverter<TaskStatus>());

            builder.Property(t => t.ExpirationUtc).HasColumnName("expiration_utc");
            builder.Property(t => t.CreatedUtc).IsRequired().HasColumnName("created_utc");
            builder.Property(t => t.UpdatedUtc).IsRequired().HasColumnName("updated_utc");
        }
    }
}
