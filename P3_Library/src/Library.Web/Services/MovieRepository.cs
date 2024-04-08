using Library.Web.Data;
using Library.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly LibContext _context;

        public MovieRepository(LibContext context) => _context = context;

        public async Task<Guid> AddAsync(Movie movie)
        {
            var entry = await _context.AddAsync(movie).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);

            return entry.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Movie.SingleAsync(m => m.Id == id).ConfigureAwait(false);
            _ = _context.Remove(entity);

            await SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movie.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(IEnumerable<Guid> tagIds, string? searchCriteria = "")
        {
            if (tagIds == null)
            {
                throw new ArgumentNullException(nameof(tagIds));
            }

            var tagIdsArray = tagIds.ToArray();
            var tagIdsPresent = tagIdsArray.Length != 0;
            var searchCriteriaPresent = !string.IsNullOrWhiteSpace(searchCriteria);
            if (!tagIdsPresent && !searchCriteriaPresent)
            {
                return await GetAllAsync().ConfigureAwait(false);
            }

            if (tagIdsPresent && searchCriteriaPresent)
            {
                return await _context.MovieTags.Include(movieTag => movieTag.Movie)
                    .Where(movieTag => tagIdsArray.Contains(movieTag.TagId))
                    .GroupBy(movieTag => movieTag.Movie, movieTag => movieTag)
                    .Where(group => group.Count() == tagIdsArray.Length)
                    .Select(group => group.Key)
                    .Where(m => m.Name.Contains(searchCriteria!))
                    .ToListAsync()
                    .ConfigureAwait(false);
            }

            if (!tagIdsPresent && searchCriteriaPresent)
            {
                return await _context.Movie.Include(m => m.MovieTags)
                    .Where(m => m.Name.Contains(searchCriteria))
                    .ToListAsync()
                    .ConfigureAwait(false);
            }

            return await _context.MovieTags.Include(movieTag => movieTag.Movie)
                .Where(movieTag => tagIdsArray.Contains(movieTag.TagId))
                .GroupBy(movieTag => movieTag.Movie, movieTag => movieTag)
                .Where(group => group.Count() == tagIdsArray.Length)
                .Select(group => group.Key)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Movie> GetByIdAsync(Guid id, bool loadNavigationProperties = false)
        {
            if (loadNavigationProperties)
            {
                return await _context.Movie
                    .Include(m => m.MovieTags)
                    .ThenInclude(mt => mt.Tag)
                    .SingleAsync(m => m.Id == id)
                    .ConfigureAwait(false);
            }

            return await _context.Movie
                .SingleAsync(m => m.Id == id)
                .ConfigureAwait(false);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
