using Library.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Web.Data
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            _ = builder.HasKey(g => g.Id);

            _ = builder.Property(g => g.Value)
                .HasMaxLength(32)
                .IsRequired();

            _ = builder.ToTable(nameof(Tag));
        }
    }
}