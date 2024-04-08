using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Components
{
    public class LoadAllTagsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TagListItemViewModel item) => View(item);
    }
}
