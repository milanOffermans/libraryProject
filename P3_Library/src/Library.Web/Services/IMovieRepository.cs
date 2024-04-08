using Library.Web.Domain;

namespace Library.Web.Services
{
    public interface IMovieRepository
    {
        Task<Guid> AddAsync(Movie movie);

        Task DeleteByIdAsync(Guid id);

        Task<IEnumerable<Movie>> GetAllAsync();

        Task<IEnumerable<Movie>> GetAllAsync(IEnumerable<Guid>? tagIds, string searchCriteria = "");

        Task<Movie> GetByIdAsync(Guid id, bool loadNavigationProperties = false);

        Task SaveChangesAsync();
    }
}
