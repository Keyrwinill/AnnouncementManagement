using System.ComponentModel.DataAnnotations;

namespace AnnouncementManagement.ViewModels
{
	public class AnnouncementEditViewModel : IValidatableObject
	{
		public int? Id { get; set; }

		[Required(ErrorMessage = "類別為必填")]
		public string Category { get; set; } = string.Empty;

		[Required(ErrorMessage = "公告標題為必填")]
		[MaxLength(200, ErrorMessage = "公告標題不可超過 200 個字元")]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "上架開始時間為必填")]
		public DateTime? PublishStart { get; set; }

		[Required(ErrorMessage = "上架結束時間為必填")]
		public DateTime? PublishEnd { get; set; }

		public bool IsPinned { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = "排序不可小於 0")]
		public int SortOrder { get; set; }

		public bool IsVisible { get; set; }

		[MaxLength(4000, ErrorMessage = "公告內容不可超過 4000 個字元")]
		public string? Content { get; set; }

		public IEnumerable<ValidationResult> Validate(
			ValidationContext validationContext)
		{
			if (PublishStart.HasValue &&
				PublishEnd.HasValue &&
				PublishStart.Value > PublishEnd.Value)
			{
				yield return new ValidationResult(
					"上架結束時間不可早於開始時間",
					new[] { nameof(PublishEnd) });
			}
		}
	}
}