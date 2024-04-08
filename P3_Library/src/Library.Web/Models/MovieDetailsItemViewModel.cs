using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models
{
    public class MovieDetailsViewModel
    {
        public IFormFile? FormImage { get; set; }

        public Guid Id { get; set; }

        public string? ImageBase64 { get; set; }

        public string? Name { get; set; }

        public List<SelectListItem> Tags { get; set; } = null!;
    }
}
