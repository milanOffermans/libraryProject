namespace Library.Web.Models
{
    public class MovieListItemViewModel
    {
        public Guid[] CheckedTags { get; set; } = { Guid.Empty };

        public Guid Id { get; set; }

        public string ImageBase64 { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? SearchString { get; set; }
    }
}
