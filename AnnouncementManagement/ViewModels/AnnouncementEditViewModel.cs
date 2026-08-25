using System.ComponentModel.DataAnnotations;

namespace AnnouncementManagement.ViewModels;

public class AnnouncementEditViewModel
{
	public int? Id { get; set; }

	[Required(ErrorMessage = "公告標題為必填")]
	[MaxLength(200)]
	public string Title { get; set; } = string.Empty;

	[Required(ErrorMessage = "類別為必填")]
	public string Category { get; set; } = string.Empty;

	[Required(ErrorMessage = "上架開始時間為必填")]
	public DateTime? PublishStart { get; set; }

	[Required(ErrorMessage = "上架結束時間為必填")]
	public DateTime? PublishEnd { get; set; }

	public bool IsPinned { get; set; }

	public int SortOrder { get; set; }

	public bool IsVisible { get; set; }

	[MaxLength(4000)]
	public string Content { get; set; } = string.Empty;
}