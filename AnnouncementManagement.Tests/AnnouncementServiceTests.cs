using AnnouncementManagement.Data;
using AnnouncementManagement.Models;
using AnnouncementManagement.Services;
using AnnouncementManagement.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementManagement.Tests;

public class AnnouncementServiceTests
{
	[Fact]
	public async Task SearchAsync_ReturnsPinnedAnnouncementsFirst()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		using var dbContext = new ApplicationDbContext(options);

		dbContext.Announcements.AddRange(
			new Announcement
			{
				Id = 1,
				Category = "技術通告",
				Title = "Normal",
				PublishStart = DateTime.Now,
				PublishEnd = DateTime.Now.AddDays(1),
				IsPinned = false,
				SortOrder = 1,
				IsVisible = true,
				Content = ""
			},
			new Announcement
			{
				Id = 2,
				Category = "技術通告",
				Title = "Pinned",
				PublishStart = DateTime.Now,
				PublishEnd = DateTime.Now.AddDays(1),
				IsPinned = true,
				SortOrder = 10,
				IsVisible = true,
				Content = ""
			});

		await dbContext.SaveChangesAsync();

		var service = new AnnouncementService(dbContext);

		// Act
		var results = await service.SearchAsync(
			null,
			null,
			null);

		// Assert
		Assert.Equal(2, results.Count);
		Assert.Equal("Pinned", results[0].Title);
		Assert.Equal("Normal", results[1].Title);
	}

	[Fact]
	public async Task CreateAsync_ValidModel_SavesAnnouncement()
	{
		// Arrange
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		using var dbContext = new ApplicationDbContext(options);

		var service = new AnnouncementService(dbContext);

		var model = new AnnouncementEditViewModel
		{
			Category = "技術通告",
			Title = "Test announcement",
			PublishStart = new DateTime(2026, 8, 27, 10, 0, 0),
			PublishEnd = new DateTime(2026, 8, 28, 10, 0, 0),
			IsPinned = true,
			SortOrder = 1,
			IsVisible = true,
			Content = "Test content"
		};

		// Act
		await service.CreateAsync(model);

		// Assert
		var announcement = await dbContext.Announcements
			.SingleAsync();

		Assert.Equal("Test announcement", announcement.Title);
		Assert.Equal("技術通告", announcement.Category);
		Assert.True(announcement.IsPinned);
		Assert.Equal(1, announcement.SortOrder);
		Assert.True(announcement.IsVisible);
		Assert.Equal("Test content", announcement.Content);
	}
}