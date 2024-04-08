using System.Collections.ObjectModel;

namespace Library.Web.Domain
{
    public class Movie
    {
        private readonly Collection<MovieTag> _movieTags;

        public Movie(string name, byte[] image)
            : this()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            if (image is null || image.Length == 0)
            {
                throw new ArgumentNullException(nameof(image));
            }

            Id = Guid.NewGuid();
            Name = name;
            Image = image;
        }

        private Movie() => _movieTags = new Collection<MovieTag>();

        public Guid Id { get; private set; }

        public byte[] Image { get; private set; } = null!;

        public IEnumerable<MovieTag> MovieTags => _movieTags;

        public string Name { get; private set; } = null!;

        public void UpdateImage(byte[] imageBytes)
        {
            if (imageBytes is null || imageBytes.Length == 0)
            {
                throw new ArgumentNullException(nameof(imageBytes));
            }

            Image = imageBytes;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
        }
    }
}
