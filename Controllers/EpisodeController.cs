/// <summary>
/// Episode Controller
/// </summary>
/// <remarks>
/// Handles episode CRUD, viewing, and search operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
using group6_Mendoza_Hontanosass__lab3.Services;
namespace group6_Mendoza_Hontanosass__lab3.Controllers
{
    public class EpisodeController : Controller
    {
        private readonly IEpisodeService _episodeService;
        private readonly IPodcastService _podcastService;
        private readonly UserManager<User> _userManager;

        public EpisodeController(
            IEpisodeService episodeService,
            IPodcastService podcastService,
            UserManager<User> userManager)
        {
            _episodeService = episodeService;
            _podcastService = podcastService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = new EpisodeSearchViewModel
            {
                Results = (await _episodeService.GetApprovedEpisodesAsync()).ToList(),
                AvailablePodcasts = (await _podcastService.GetApprovedPodcastsAsync()).ToList()
            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Search(EpisodeSearchViewModel model)
        {
            model.Results = (await _episodeService.SearchEpisodesAsync(model)).ToList();
            model.AvailablePodcasts = (await _podcastService.GetApprovedPodcastsAsync()).ToList();
            return View("Index", model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var userId = _userManager.GetUserId(User);
            var model = await _episodeService.GetEpisodeDetailsAsync(id, userId);

            await _episodeService.IncrementViewAsync(id);

            return View(model);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpGet]
        public async Task<IActionResult> Create(int? podcastId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var podcasts = await _podcastService.GetPodcastsByCreatorAsync(userId);
            ViewBag.Podcasts = podcasts;

            var model = new EpisodeViewModel();
            if (podcastId.HasValue)
            {
                model.PodcastID = podcastId.Value;
            }

            return View(model);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EpisodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var podcast = await _podcastService.GetPodcastByIdAsync(model.PodcastID);
                    var userId = _userManager.GetUserId(User);

                    if (podcast == null)
                    {
                        ModelState.AddModelError(string.Empty, "Podcast not found.");
                    }
                    else if (podcast.CreatorID != userId && !User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }
                    else
                    {
                        await _episodeService.CreateEpisodeAsync(model);
                        TempData["Success"] = "Episode created successfully!";
                        return RedirectToAction("Details", "Podcast", new { id = model.PodcastID });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error creating episode: {ex.Message}");
                }
            }

            var userIdForPodcasts = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userIdForPodcasts))
            {
                ViewBag.Podcasts = await _podcastService.GetPodcastsByCreatorAsync(userIdForPodcasts);
            }
            return View(model);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var episode = await _episodeService.GetEpisodeByIdAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            var podcast = await _podcastService.GetPodcastByIdAsync(episode.PodcastID);
            var userId = _userManager.GetUserId(User);
            if (podcast?.CreatorID != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var model = new EpisodeViewModel
            {
                EpisodeID = episode.EpisodeID,
                PodcastID = episode.PodcastID,
                Title = episode.Title,
                Description = episode.Description,
                ReleaseDate = episode.ReleaseDate,
                Duration = episode.Duration,
                ExistingAudioFileURL = episode.AudioFileURL,
                ExistingThumbnailURL = episode.ThumbnailURL
            };

            ViewBag.PodcastTitle = podcast?.Title;
            return View(model);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EpisodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var episode = await _episodeService.GetEpisodeByIdAsync(model.EpisodeID!.Value);
                    if (episode == null)
                    {
                        return NotFound();
                    }

                    var podcast = await _podcastService.GetPodcastByIdAsync(episode.PodcastID);
                    var userId = _userManager.GetUserId(User);
                    if (podcast?.CreatorID != userId && !User.IsInRole("Admin"))
                    {
                        return Forbid();
                    }

                    await _episodeService.UpdateEpisodeAsync(model);
                    TempData["Success"] = "Episode updated successfully!";
                    return RedirectToAction(nameof(Details), new { id = model.EpisodeID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error updating episode: {ex.Message}");
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var episode = await _episodeService.GetEpisodeByIdAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            var podcast = await _podcastService.GetPodcastByIdAsync(episode.PodcastID);
            var userId = _userManager.GetUserId(User);
            if (podcast?.CreatorID != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(episode);
        }

        [Authorize(Roles = "Podcaster,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var episode = await _episodeService.GetEpisodeByIdAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            var podcast = await _podcastService.GetPodcastByIdAsync(episode.PodcastID);
            var userId = _userManager.GetUserId(User);
            if (podcast?.CreatorID != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var podcastId = episode.PodcastID;
            await _episodeService.DeleteEpisodeAsync(id);
            TempData["Success"] = "Episode deleted successfully!";
            return RedirectToAction("Details", "Podcast", new { id = podcastId });
        }

        [HttpPost]
        public async Task<IActionResult> IncrementPlayCount(int id)
        {
            await _episodeService.IncrementPlayCountAsync(id);
            return Ok();
        }

    }
}
