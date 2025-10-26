/// <summary>
/// Podcast Controller
/// </summary>
/// <remarks>
/// Handles podcast CRUD operations for podcasters
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>1.0</version>
/// <date>2025-10-24</date>
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
using group6_Mendoza_Hontanosass__lab3.Services;
namespace group6_Mendoza_Hontanosass__lab3.Controllers
{
    public class PodcastController : Controller
    {
        private readonly IPodcastService _podcastService;
        private readonly UserManager<User> _userManager;

        public PodcastController(IPodcastService podcastService, UserManager<User> userManager)
        {
            _podcastService = podcastService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var podcasts = await _podcastService.GetApprovedPodcastsAsync();
            return View(podcasts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var podcast = await _podcastService.GetPodcastByIdAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }

            return View(podcast);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PodcastViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(User);
                    if (string.IsNullOrEmpty(userId))
                    {
                        return Unauthorized();
                    }

                    await _podcastService.CreatePodcastAsync(model, userId);
                    TempData["Success"] = "Podcast created successfully!";
                    return RedirectToAction(nameof(MyPodcasts));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error creating podcast: {ex.Message}");
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var podcast = await _podcastService.GetPodcastByIdAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (podcast.CreatorID != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var model = new PodcastViewModel
            {
                PodcastID = podcast.PodcastID,
                Title = podcast.Title,
                Description = podcast.Description,
                ExistingThumbnailURL = podcast.ThumbnailURL
            };

            return View(model);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PodcastViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var podcast = await _podcastService.GetPodcastByIdAsync(model.PodcastID!.Value);
                    if (podcast == null)
                    {
                        return NotFound();
                    }

                    var userId = _userManager.GetUserId(User);
                    if (podcast.CreatorID != userId && !User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }

                    await _podcastService.UpdatePodcastAsync(model);
                    TempData["Success"] = "Podcast updated successfully!";
                    return RedirectToAction(nameof(MyPodcasts));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error updating podcast: {ex.Message}");
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var podcast = await _podcastService.GetPodcastByIdAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (podcast.CreatorID != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(podcast);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podcast = await _podcastService.GetPodcastByIdAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (podcast.CreatorID != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            await _podcastService.DeletePodcastAsync(id);
            TempData["Success"] = "Podcast deleted successfully!";
            return RedirectToAction(nameof(MyPodcasts));
        }

        [Authorize(Roles = "Podcaster")]
        public async Task<IActionResult> MyPodcasts()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var podcasts = await _podcastService.GetPodcastsByCreatorAsync(userId);
            return View(podcasts);
        }
    }
}
