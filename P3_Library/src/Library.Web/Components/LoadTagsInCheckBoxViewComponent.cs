using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.MovieComponents
{
    public class LoadTagsInCheckBoxViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TagListItemViewModel item) => View(item);
    }
}
