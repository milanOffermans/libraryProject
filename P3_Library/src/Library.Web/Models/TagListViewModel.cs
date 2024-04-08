namespace Library.Web.Models
{
    public class TagListViewModel
    {
        public TagListViewModel(IEnumerable<TagListItemViewModel> items) => Tags = items;

        public IEnumerable<TagListItemViewModel> Tags { get; set; }
    }
}
