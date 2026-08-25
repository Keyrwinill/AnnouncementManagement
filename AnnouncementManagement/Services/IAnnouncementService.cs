using AnnouncementManagement.ViewModels;

namespace AnnouncementManagement.Services;

public interface IAnnouncementService
{
	Task<List<AnnouncementListItemViewModel>> SearchAsync(
		DateTime? publishStart,
		DateTime? publishEnd,
		string? title);

	Task CreateAsync(AnnouncementEditViewModel model);

	Task<AnnouncementEditViewModel?> GetByIdAsync(int id);

	Task<bool> UpdateAsync(AnnouncementEditViewModel model);
}
