using HikeHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HikeHub.Services
{
    public class TagService : ITagService
    {
        private readonly HikeHubDbContext _context;

        public TagService(HikeHubDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetTagByIdAsync(int id)
        {
			var tag = await _context.Tags.FirstOrDefaultAsync(tag => tag.TagID == id);
			if (tag == null)
			{
				throw new ArgumentNullException(nameof(tag), "Announcement is null.");
			}
			return tag;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }
        public string GetTagTypeString(Tag.TagType type)
        {
            return type.ToString();
        }
		public async Task<List<Tag>> GetTagsByTypeAsync(Tag.TagType tagType)
        {
            return await _context.Tags
                .Where(tag => tag.TagName == tagType)
                .ToListAsync();
        }
		public List<string> GetAllTagTypes()
        {
            return Enum.GetNames(typeof(Tag.TagType)).ToList();
        }

    }
}
