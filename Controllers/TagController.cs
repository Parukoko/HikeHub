using HikeHub.Models;
using HikeHub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HikeHub.Controllers
{
    public class SearchController : Controller
    {
        private readonly ITagService _tagService;

        public SearchController(ITagService tagService)
        {
            _tagService = tagService;
        }

		public async Task<IActionResult> Index()
		{
			var tags = await _tagService.GetAllTagsAsync();
			var tagNames = tags.Select(tag => new SelectListItem
			{
				Value = tag.TagID.ToString(),
				Text = tag.GetTagNameString()
			}).ToList();

			ViewBag.Tags = tagNames;

			return View();
		}


        private static List<SelectListItem> GetTagsSelectList(Tag.TagType selectedType)
        {
            return Enum.GetValues<Tag.TagType>()
                .Select(t => new SelectListItem
                {
                    Value = ((int)t).ToString(),
                    Text = t.ToString(),
                    Selected = t == selectedType
                }).ToList();
        }
    }
}
