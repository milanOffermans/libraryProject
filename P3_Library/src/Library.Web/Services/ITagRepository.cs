using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Library.Web.Domain;

namespace Library.Web.Services
{
    public interface ITagRepository
    {
        Task AddTagAsync(Tag tag);

        Task<Tag> FindTagById(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<Tag>> GetAllAsync(Expression<Func<Tag, object>> orderExpression);

        Task<IEnumerable<Tag>> GetAllAsync();

        IEnumerable<Guid> GetAllTagIdsByNameAsync(Guid[] tagName);

        void RemoveTag(Tag entity);
    }
}
