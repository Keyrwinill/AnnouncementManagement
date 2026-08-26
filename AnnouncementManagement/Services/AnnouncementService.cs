using AnnouncementManagement.Data;
using AnnouncementManagement.Models;
using AnnouncementManagement.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementManagement.Services;

public class AnnouncementService : IAnnouncementService
{
	private readonly ApplicationDbContext _dbContext;

	public AnnouncementService(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<List<AnnouncementListItemViewModel>> SearchAsync(
		DateTime? publishStart,
		DateTime? publishEnd,
		string? title)
	{
		var query = _dbContext.Announcements
			.AsNoTracking()
			.AsQueryable();

		if (publishStart.HasValue)
		{
			// Announcement.Start >= Search.Start
			var start = publishStart.Value.Date;

			query = query.Where(x =>
				x.PublishStart >= start);
		}
		
		if (publishEnd.HasValue)
		{
			// Announcement.Start <= Search.End
			var endExclusive = publishEnd.Value.Date.AddDays(1);

			query = query.Where(x =>
				x.PublishStart <= endExclusive);
		}

		if (!string.IsNullOrWhiteSpace(title))
		{
			query = query.Where(x =>
				x.Title.Contains(title));
		}

		return await query
			.AsNoTracking()
			.OrderByDescending(x => x.IsPinned)
			.ThenBy(x => x.SortOrder)
			.ThenByDescending(x => x.PublishStart)
			.Select(x => new AnnouncementListItemViewModel
			{
				Id = x.Id,
				Category = x.Category,
				Title = x.Title,
				PublishStart = x.PublishStart,
				PublishEnd = x.PublishEnd,
				IsPinned = x.IsPinned,
				SortOrder = x.SortOrder,
				IsVisible = x.IsVisible
			})
			.ToListAsync();
	}

	public async Task CreateAsync(AnnouncementEditViewModel model)
	{
		var announcement = new Announcement
		{
			Category = model.Category,
			Title = model.Title,
			PublishStart = model.PublishStart!.Value,
			PublishEnd = model.PublishEnd!.Value,
			IsPinned = model.IsPinned,
			SortOrder = model.SortOrder,
			IsVisible = model.IsVisible,
			Content = model.Content
		};

		_dbContext.Announcements.Add(announcement);

		await _dbContext.SaveChangesAsync();
	}

	public async Task<AnnouncementEditViewModel?> GetByIdAsync(int id)
	{
		return await _dbContext.Announcements
			.AsNoTracking()
			.Where(x => x.Id == id)
			.Select(x => new AnnouncementEditViewModel
			{
				Id = x.Id,
				Category = x.Category,
				Title = x.Title,
				PublishStart = x.PublishStart,
				PublishEnd = x.PublishEnd,
				IsPinned = x.IsPinned,
				SortOrder = x.SortOrder,
				IsVisible = x.IsVisible,
				Content = x.Content
			})
			.FirstOrDefaultAsync();
	}

	public async Task<bool> UpdateAsync(AnnouncementEditViewModel model)
	{
		if (!model.Id.HasValue)
		{
			return false;
		}

		var announcement = await _dbContext.Announcements
			.FirstOrDefaultAsync(x => x.Id == model.Id.Value);

		if (announcement == null)
		{
			return false;
		}

		announcement.Category = model.Category;
		announcement.Title = model.Title;
		announcement.PublishStart = model.PublishStart!.Value;
		announcement.PublishEnd = model.PublishEnd!.Value;
		announcement.IsPinned = model.IsPinned;
		announcement.SortOrder = model.SortOrder;
		announcement.IsVisible = model.IsVisible;
		announcement.Content = model.Content;

		await _dbContext.SaveChangesAsync();

		return true;
	}
}