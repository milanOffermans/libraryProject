using Library.Web.Domain;
using Library.Web.Extensions;
using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Controllers
{
    public partial class HomeController
    {
        private static bool IsValid(Guid? id) => id != null && id.Value != Guid.Empty;

        private async Task<IActionResult> CreateMovieAndRedirectAsync(MovieDetailsViewModel updatedModel)
        {
            var imageBytes = await TryGetImageBytesOrPlaceholderAsync(updatedModel).ConfigureAwait(false);

            var movie = new Movie(updatedModel.Name, imageBytes);
            var id = await _movieRepository.AddAsync(movie).ConfigureAwait(false);

            updatedModel.ImageBase64 =
                await _imageService.GetBase64ImageStringAsync(movie.Image).ConfigureAwait(false);

            return RedirectToAction("Details", new { id });
        }

        private async Task<List<SelectListItem>> LoadTagsAsync(Movie entity)
        {
            var usedTags = entity.MovieTags.Select(mt => mt.Tag.Value);
            var tags = await _tagRepository.GetAllAsync(t => t.Value).ConfigureAwait(false);

            return tags.Select(t => new SelectListItem
            {
                Text = t.Value,
                Value = t.Id.ToString(),
                Selected = usedTags.Contains(t.Value)
            }).ToList();
        }

        private async Task<byte[]> TryGetImageBytesOrPlaceholderAsync(MovieDetailsViewModel model)
        {
            return model.FormImage == null || model.FormImage.Length == 0
                ? await _imageService.GetPlaceholderImageBytesAsync().ConfigureAwait(false)
                : await model.FormImage.GetFileBytesAsync().ConfigureAwait(false);
        }
    }
}
