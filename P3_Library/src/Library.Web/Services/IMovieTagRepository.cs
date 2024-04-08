using Library.Web.Domain;

namespace Library.Web.Services
{
    public interface IMovieTagRepository
    {
        Task SaveChangesAsync();

        Task UpdateMovieAsync(Guid movieId, IEnumerable<MovieTag> collectionToInsert);
    }
}
