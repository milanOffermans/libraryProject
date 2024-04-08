using Library.Web.Domain;
using Library.Web.Models;
using Library.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository, IMovieRepository movieRepository)
        {
            _tagRepository = tagRepository;
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TagListItemViewModel listItemViewModel)
        {
            if (listItemViewModel == null)
            {
                throw new ArgumentNullException(nameof(listItemViewModel));
            }

            if (listItemViewModel.Value is { Length: > 32 })
            {
                var newName = listItemViewModel.Value[..32];
                listItemViewModel.Value = newName;
            }

            var newTag = new Tag(listItemViewModel.Value);
            await _tagRepository.AddTagAsync(newTag).ConfigureAwait(false);
            await _movieRepository.SaveChangesAsync().ConfigureAwait(false);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<string> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _tagRepository.FindTagById(id, cancellationToken).ConfigureAwait(false);
            _tagRepository.RemoveTag(entity);
            await _movieRepository.SaveChangesAsync().ConfigureAwait(false);

            return entity.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tags = await _tagRepository.GetAllAsync().ConfigureAwait(false);
            var tagList = tags.Select(tag => new TagListItemViewModel
            { Id = tag.Id, Value = tag.Value, });

            return View(new TagListViewModel(tagList.OrderBy(m => m.Value)));
        }
    }
}
