using Library.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Web.Data
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            _ = builder.HasKey(g => g.Id);

            _ = builder.Property(g => g.Name)
                .HasMaxLength(32)
                .IsRequired();

            _ = builder.Property(g => g.Image)
                .HasMaxLength(1 << 20);

            _ = builder.ToTable(nameof(Movie));
        }
    }
}