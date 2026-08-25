using AnnouncementManagement.Services;
using AnnouncementManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementManagement.Controllers;

public class AnnouncementController : Controller
{
	private readonly IAnnouncementService _announcementService;

	public AnnouncementController(
		IAnnouncementService announcementService)
	{
		_announcementService = announcementService;
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
			IsVisible = true
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

		if (model.PublishStart > model.PublishEnd)
		{
			ModelState.AddModelError(
				nameof(model.PublishEnd),
				"上架結束時間不可早於開始時間");

			return View(model);
		}

		await _announcementService.CreateAsync(model);

		return RedirectToAction(nameof(Index));
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

		if (model.PublishStart > model.PublishEnd)
		{
			ModelState.AddModelError(
				nameof(model.PublishEnd),
				"上架結束時間不可早於開始時間");

			return View(model);
		}

		var success = await _announcementService.UpdateAsync(model);

		if (!success)
		{
			return NotFound();
		}

		return RedirectToAction(nameof(Index));
	}
}