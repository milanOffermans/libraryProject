using System.Diagnostics;
using Library.Web.Domain;
using Library.Web.Extensions;
using Library.Web.Models;
using Library.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieTagRepository _movieTagRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(
            IImageService imageService,
            IMovieRepository movieRepository,
            IMovieTagRepository movieTagRepository,
            ITagRepository tagRepository)
        {
            _imageService = imageService;
            _movieRepository = movieRepository;
            _movieTagRepository = movieTagRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _movieRepository.DeleteByIdAsync(id).ConfigureAwait(false);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            MovieDetailsViewModel movie;

            if (IsValid(id))
            {
                var entity = await _movieRepository.GetByIdAsync(id!.Value, true).ConfigureAwait(false);
                movie = new MovieDetailsViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    ImageBase64 = await _imageService.GetBase64ImageStringAsync(entity.Image).ConfigureAwait(false),
                    Tags = await LoadTagsAsync(entity).ConfigureAwait(false)
                };
            }
            else
            {
                movie = new MovieDetailsViewModel
                {
                    ImageBase64 = await _imageService
                        .GetBase64ImageStringAsync(null)
                        .ConfigureAwait(false),
                    Tags = new List<SelectListItem>()
                };
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Details(Guid? id, MovieDetailsViewModel updatedModel)
        {
            if (updatedModel == null)
            {
                throw new ArgumentNullException(nameof(updatedModel));
            }

            if (!updatedModel.FormImage.IsImage())
            {
                updatedModel.FormImage = null;
            }

            if (updatedModel.Name is { Length: > 32 })
            {
                var newName = updatedModel.Name[..32];
                updatedModel.Name = newName;
            }

            if (!IsValid(id))
            {
                return await CreateMovieAndRedirectAsync(updatedModel).ConfigureAwait(false);
            }

            // load movie
            var entity = await _movieRepository
                .GetByIdAsync(id!.Value, true).ConfigureAwait(false);

            // update name
            if (updatedModel.Name != null)
            {
                entity.UpdateName(updatedModel.Name);
            }

            // update movie image
            var formImage = updatedModel.FormImage;
            var movieImageBytes = await formImage.TryGetFileBytesAsync().ConfigureAwait(false);

            if (movieImageBytes != null)
            {
                entity.UpdateImage(movieImageBytes);
                updatedModel.ImageBase64 = _imageService.ConvertImageBytesToBase64ImageString(movieImageBytes);
            }
            else
            {
                updatedModel.ImageBase64 =
                    await _imageService.GetBase64ImageStringAsync(entity.Image).ConfigureAwait(false);
            }

            await _movieRepository.SaveChangesAsync().ConfigureAwait(false);

            // update movie tags
            var newMovieTags = CreateMovieTags(updatedModel, entity);
            await _movieTagRepository.UpdateMovieAsync(updatedModel.Id, newMovieTags).ConfigureAwait(false);
            await _movieTagRepository.SaveChangesAsync().ConfigureAwait(false);

            return View(updatedModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = await _movieRepository.GetAllAsync().ConfigureAwait(false);
            var tags = await _tagRepository.GetAllAsync().ConfigureAwait(false);
            var movieList = new List<MovieListItemViewModel>();

            foreach (var movie in movies)
            {
                await SetMovieListViewModel(movieList, movie).ConfigureAwait(false);
            }

            var tagList = tags.Select(tag => new TagListItemViewModel()
            {
                Id = tag.Id,
                Value = tag.Value,
            }).OrderBy(t => t.Value).Distinct().ToArray();

            return View(new MovieListViewModel(movieList.OrderBy(m => m.Name), tagList));
        }

        [HttpPost]
        public async Task<IActionResult> Index(MovieListItemViewModel newMovieList)
        {
            if (newMovieList == null)
            {
                throw new ArgumentNullException(nameof(newMovieList));
            }

            var searchString = newMovieList.SearchString;
            var selectedTagId = _tagRepository.GetAllTagIdsByNameAsync(newMovieList.CheckedTags);
            var movies = await _movieRepository.GetAllAsync(selectedTagId, searchString).ConfigureAwait(false);
            var tags = await _tagRepository.GetAllAsync().ConfigureAwait(false);
            var movieList = new List<MovieListItemViewModel>();

            foreach (var movie in movies)
            {
                await SetMovieListViewModel(movieList, movie).ConfigureAwait(false);
            }

            var tagList = tags.Select(tag => new TagListItemViewModel() { Id = tag.Id, Value = tag.Value, }).OrderBy(t => t.Value).Distinct().ToArray();

            return View(new MovieListViewModel(movieList.OrderBy(m => m.Name), tagList));
        }

        private static IEnumerable<MovieTag> CreateMovieTags(MovieDetailsViewModel updatedModel, Movie entity)
        {
            var newMovieTags = updatedModel.Tags
                .Where(vm => vm.Selected)
                .Select(t => new MovieTag(entity.Id, Guid.Parse(t.Value))).ToList();

            return newMovieTags;
        }

        private async Task SetMovieListViewModel(List<MovieListItemViewModel> movieList, Movie? movie)
        {
            if (movie != null)
            {
                var item = new MovieListItemViewModel
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    ImageBase64 = await _imageService.GetBase64ImageStringAsync(movie.Image).ConfigureAwait(false)
                };
                movieList.Add(item);
            }
        }
    }
}
