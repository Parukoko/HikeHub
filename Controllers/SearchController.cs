using HikeHub.Models;
using HikeHub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
public class SearchController : Controller
{
    private readonly ITagService _tagService;

    public SearchController(ITagService tagService)
    {
        _tagService = tagService;
    }

    public IActionResult Index()
    {
        ViewBag.Tags = Enum.GetValues<Tag.TagType>()
            .Select(t => new SelectListItem
            {
                Value = ((int)t).ToString(),
                Text = t.ToString()
            }).ToList();

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Results(Tag.TagType? region)
    {
        if (!region.HasValue)
        {
            return RedirectToAction(nameof(Index));
        }

        var tags = await _tagService.GetTagsByTypeAsync(region.Value);
        ViewBag.SelectedRegion = _tagService.GetTagTypeString(region.Value);
        ViewBag.Tags = GetTagsSelectList(region.Value);  // Call the static method here

        return View(tags);
    }

    [HttpGet]
    public async Task<IActionResult> ByRegion(int? tagId)
    {
        if (!tagId.HasValue)
        {
            return RedirectToAction(nameof(Index));
        }

        var tag = await _tagService.GetTagByIdAsync(tagId.Value);
        if (tag == null)
        {
            return NotFound();
        }

        ViewBag.RegionName = _tagService.GetTagTypeString(tag.TagName);
        ViewBag.Tags = GetTagsSelectList(tag.TagName);  // Call the static method here

        return View(tag);
    }

    // This is the static method you asked about
    private static List<SelectListItem> GetTagsSelectList(Tag.TagType selectedType)
    {
        return Enum.GetValues<Tag.TagType>()
            .Cast<Tag.TagType>()
            .Select(t => new SelectListItem
            {
                Value = ((int)t).ToString(),
                Text = t.ToString(),
                Selected = t == selectedType
            }).ToList();
    }
}
