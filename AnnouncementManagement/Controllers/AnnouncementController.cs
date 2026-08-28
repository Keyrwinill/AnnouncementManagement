using AnnouncementManagement.Services;
using AnnouncementManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementManagement.Controllers;

public class AnnouncementController : Controller
{
	private readonly IAnnouncementService _announcementService;
	private readonly ILogger<AnnouncementController> _logger;

	public AnnouncementController(
		IAnnouncementService announcementService,
		ILogger<AnnouncementController> logger)
	{
		_announcementService = announcementService;
		_logger = logger;
	}

	[HttpGet]
	public async Task<IActionResult> Index(
		DateTime? publishStart,
		DateTime? publishEnd,
		string? title)
	{
		var model = new AnnouncementSearchViewModel
		{
			PublishStart = publishStart,
			PublishEnd = publishEnd,
			Title = title
		};

		if (publishStart.HasValue &&
			publishEnd.HasValue &&
			publishStart.Value.Date > publishEnd.Value.Date)
		{
			ModelState.AddModelError(
				nameof(model.PublishEnd),
				"查詢結束日期不可早於開始日期");

			return View(model);
		}

		model.Results = await _announcementService.SearchAsync(
			publishStart,
			publishEnd,
			title);

		return View(model);
	}

	[HttpGet]
	// Display the empty form
	public IActionResult Create()
	{
		var model = new AnnouncementEditViewModel
		{
			IsPublish = true
		};

		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	// Receives the submitted form
	public async Task<IActionResult> Create(AnnouncementEditViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		try
		{
			await _announcementService.CreateAsync(model);

			return RedirectToAction(nameof(Index));
		}
		catch (Exception ex)
		{
			_logger.LogError(
				ex,
				"Failed to create announcement.");

			ModelState.AddModelError(
				string.Empty,
				"新增公告時發生錯誤，請稍後再試");

			return View(model);
		}
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		var model = await _announcementService.GetByIdAsync(id);

		if (model == null)
		{
			return NotFound();
		}

		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(
	AnnouncementEditViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		try
		{
			var success = await _announcementService.UpdateAsync(model);

			if (!success)
			{
				return NotFound();
			}

			return RedirectToAction(nameof(Index));
		}
		catch (Exception ex)
		{
			_logger.LogError(
				ex,
				"Failed to update announcement. Id: {AnnouncementId}",
				model.Id);

			ModelState.AddModelError(
				string.Empty,
				"修改公告時發生錯誤，請稍後再試");

			return View(model);
		}
	}
}