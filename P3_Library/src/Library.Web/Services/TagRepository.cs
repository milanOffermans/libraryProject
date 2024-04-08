using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using Library.Web.Data;
using Library.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Services
{
    public class TagRepository : ITagRepository
    {
        private readonly LibContext _context;

        public TagRepository(LibContext context) => _context = context;

        public async Task AddTagAsync(Tag tag)
        {
            await _context.Tag.AddAsync(tag);
        }

        public async Task<Tag> FindTagById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Tag.SingleAsync(m => m.Id == id, cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(Expression<Func<Tag, object>> orderExpression) =>
                    await _context.Tag
                .OrderBy(orderExpression)
                .ToListAsync().ConfigureAwait(false);

        public async Task<IEnumerable<Tag>> GetAllAsync() => await _context.Tag.ToListAsync().ConfigureAwait(false);

        public IEnumerable<Guid>? GetAllTagIdsByNameAsync(Guid[] tagName)
        {
            var tags = new List<Guid>();
            if (tagName == null)
            {
                return tags;
            }

            tags.AddRange(tagName.SelectMany(tag => _context.Tag.Where(m => m.Id == tag).Select(m => m.Id)));

            return tags;
        }

        public void RemoveTag(Tag entity)
        {
            _context.Remove(entity);
        }
    }
}