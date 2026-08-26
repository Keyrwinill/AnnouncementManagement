using System.ComponentModel.DataAnnotations;

namespace AnnouncementManagement.Models;

public class Announcement
{
	public int Id { get; set; }

	[MaxLength(50)]
	public string Category { get; set; } = string.Empty;

	[MaxLength(200)]
	public string Title { get; set; } = string.Empty;

	public DateTime PublishStart { get; set; }

	public DateTime PublishEnd { get; set; }

	public bool IsPinned { get; set; }

	public int SortOrder { get; set; }

	public bool IsVisible { get; set; }

	[MaxLength(4000)]
	public string? Content { get; set; }
}