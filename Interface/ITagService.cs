using HikeHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HikeHub.Services
{
	public interface ITagService
	{
		Task<Tag> GetTagByIdAsync(int id);
		Task<List<Tag>> GetAllTagsAsync();
		string GetTagTypeString(Tag.TagType type);
		Task<List<Tag>> GetTagsByTypeAsync(Tag.TagType tagType);
		List<string> GetAllTagTypes();
		
	}
}
