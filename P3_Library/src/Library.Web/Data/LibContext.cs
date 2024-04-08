using Library.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Data
{
    public class LibContext : DbContext
    {
        public LibContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movie { get; private set; } = default!;

        public DbSet<MovieTag> MovieTags { get; private set; } = default!;

        public DbSet<Tag> Tag { get; private set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            _ = modelBuilder.ApplyConfiguration(new MovieConfiguration());
            _ = modelBuilder.ApplyConfiguration(new TagConfiguration());
            _ = modelBuilder.ApplyConfiguration(new MovieTagConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
