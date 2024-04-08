using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models
{
    public class TagListItemViewModel
    {
        public Guid Id { get; set; }

        public string Value { get; set; } = null!;
    }
}
