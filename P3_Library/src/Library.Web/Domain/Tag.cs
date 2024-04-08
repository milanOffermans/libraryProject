using System.Collections.ObjectModel;

namespace Library.Web.Domain
{
    public class Tag
    {
        private readonly Collection<MovieTag> _movieTags;

        public Tag(string value)
            : this()
        {
            Id = Guid.NewGuid();
            Value = value;
        }

        private Tag() => _movieTags = new Collection<MovieTag>();

        public Guid Id { get; private set; }

        public IEnumerable<MovieTag> MovieTags => _movieTags;

        public string Value { get; private set; } = null!;
    }
}
