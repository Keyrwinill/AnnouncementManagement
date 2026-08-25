namespace AnnouncementManagement.ViewModels;

public class AnnouncementSearchViewModel
{
	public DateTime? PublishStart { get; set; }

	public DateTime? PublishEnd { get; set; }

	public string? Title { get; set; }

	public List<AnnouncementListItemViewModel> Results { get; set; }
		= new();
}

public class AnnouncementListItemViewModel
{
	public int Id { get; set; }

	public string Category { get; set; } = string.Empty;

	public string Title { get; set; } = string.Empty;

	public DateTime PublishStart { get; set; }

	public DateTime PublishEnd { get; set; }

	public bool IsPinned { get; set; }

	public int SortOrder { get; set; }

	public bool IsVisible { get; set; }
}