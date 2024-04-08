namespace Library.Web.Domain
{
    public class MovieTag
    {
        public MovieTag(Guid movieId, Guid tagId)
        {
            MovieId = movieId;
            TagId = tagId;
        }

        private MovieTag()
        {
        }

        public Movie Movie { get; private set; } = null!;

        public Guid MovieId { get; private set; }

        public Tag Tag { get; private set; } = null!;

        public Guid TagId { get; set; }
    }
}
