using AnnouncementManagement.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace AnnouncementManagement.Tests;

public class AnnouncementEditViewModelTests
{
	[Fact]
	public void Validate_WhenPublishEndIsEarlierThanPublishStart_ReturnsValidationError()
	{
		// Arrange
		var model = new AnnouncementEditViewModel
		{
			Category = "技術通告",
			Title = "Test",
			PublishStart = new DateTime(2026, 8, 27, 10, 0, 0),
			PublishEnd = new DateTime(2026, 8, 26, 10, 0, 0),
			SortOrder = 1
		};

		var validationContext = new ValidationContext(model);

		// Act
		var results = model
			.Validate(validationContext)
			.ToList();

		// Assert
		Assert.Single(results);

		Assert.Equal(
			"上架結束時間不可早於開始時間",
			results[0].ErrorMessage);

		Assert.Contains(
			nameof(model.PublishEnd),
			results[0].MemberNames);
	}

	[Fact]
	public void Validate_WhenPublishEndIsAfterPublishStart_ReturnsNoValidationError()
	{
		// Arrange
		var model = new AnnouncementEditViewModel
		{
			Category = "技術通告",
			Title = "Test",
			PublishStart = new DateTime(2026, 8, 26, 10, 0, 0),
			PublishEnd = new DateTime(2026, 8, 27, 10, 0, 0),
			SortOrder = 1
		};

		var validationContext = new ValidationContext(model);

		// Act
		var results = model
			.Validate(validationContext)
			.ToList();

		// Assert
		Assert.Empty(results);
	}
}