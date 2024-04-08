using Library.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Web.Data
{
    public class MovieTagConfiguration : IEntityTypeConfiguration<MovieTag>
    {
        public void Configure(EntityTypeBuilder<MovieTag> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            _ = builder.HasKey(bc => new { bc.MovieId, bc.TagId });
            _ = builder
                .HasOne(bc => bc.Movie)
                .WithMany(b => b.MovieTags)
                .HasForeignKey(bc => bc.MovieId);
            _ = builder
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.MovieTags)
                .HasForeignKey(bc => bc.TagId);

            _ = builder.ToTable(nameof(MovieTag));
        }
    }
}
