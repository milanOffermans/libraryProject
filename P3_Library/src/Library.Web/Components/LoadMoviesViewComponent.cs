using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.MovieComponents
{
    public class LoadMoviesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(MovieListItemViewModel item) => View(item);
    }
}
