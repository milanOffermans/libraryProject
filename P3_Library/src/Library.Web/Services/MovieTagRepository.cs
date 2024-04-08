using Library.Web.Data;
using Library.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Services
{
    public class MovieTagRepository : IMovieTagRepository
    {
        private readonly LibContext _context;

        public MovieTagRepository(LibContext context) => _context = context;

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync().ConfigureAwait(false);

        public async Task UpdateMovieAsync(Guid movieId, IEnumerable<MovieTag> collectionToInsert)
        {
            var collectionToRemove = await _context.MovieTags.Where(i => i.MovieId.Equals(movieId)).ToListAsync()
                .ConfigureAwait(false);

            _context.MovieTags.RemoveRange(collectionToRemove);
            _context.MovieTags.AddRange(collectionToInsert);

            await SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
