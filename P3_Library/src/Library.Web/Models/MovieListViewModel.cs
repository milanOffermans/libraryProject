namespace Library.Web.Models
{
    public class MovieListViewModel
    {
        public MovieListViewModel(IEnumerable<MovieListItemViewModel> items, TagListItemViewModel[] tags)
        {
            Items = items;
            Tags = tags;
        }

        public IEnumerable<MovieListItemViewModel> Items { get; set; }

        public TagListItemViewModel[] Tags { get; set; }
    }
}
