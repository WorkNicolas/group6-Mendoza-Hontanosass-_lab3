using group6_Mendoza_Hontanosass__lab3.Data.Repositories;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
using group6_Mendoza_Hontanosass__lab3.Services;
/// <summary>
/// Admin Controller
/// </summary>
/// <remarks>
/// Handles administrative operations: user management, content approval
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>1.0</version>
/// <date>2025-10-26</date>
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace group6_Mendoza_Hontanosass__lab3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // IUserRepository is no longer needed for these actions
        private readonly IPodcastService _podcastService;
        private readonly IEpisodeService _episodeService;
        private readonly UserManager<User> _userManager;

        public AdminController(
            // IUserRepository userRepository, // Removed
            IPodcastService podcastService,
            IEpisodeService episodeService,
            UserManager<User> userManager)
        {
            // _userRepository = userRepository; // Removed
            _podcastService = podcastService;
            _episodeService = episodeService;
            _userManager = userManager;
        }

        // Admin panel home
        public IActionResult Index()
        {
            return View();
        }

        // List all users with management options
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = users.Select(u => new UserManagementViewModel
            {
                UserID = u.Id,
                Username = u.UserName,
                Email = u.Email,
                FullName = u.FullName,
                Role = u.Role,
                CreatedDate = u.CreatedDate,
                PodcastCount = _context.Podcasts.Count(p => p.CreatorID == u.Id), // TODO: CS0103 _context
                IsLocked = u.LockoutEnd != null && u.LockoutEnd > DateTimeOffset.UtcNow
            }).ToList();
            return View(model);
        }

        // Lock a user's account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        // Unlock a user's account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        // Delete a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            if (user.Id == currentUserId)
            {
                TempData["ErrorMessage"] = "You cannot delete your own account.";
                return RedirectToAction(nameof(ManageUsers));
            }

            // Delete related data
            var podcasts = await _podcastRepository.GetByCreatorIdAsync(id); // TODO: CS0103 _podcastRepository
            foreach (var podcast in podcasts)
            {
                await _podcastService.DeletePodcastAsync(podcast.PodcastID);
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "Failed to delete user: " + string.Join(", ", result.Errors.Select(e => e.Description));
            }
            return RedirectToAction(nameof(ManageUsers));
        }


        // List unapproved podcasts
        public async Task<IActionResult> ApprovePodcasts()
        {
            var allPodcasts = await _podcastService.GetAllPodcastsAsync();
            var unapprovedPodcasts = allPodcasts.Where(p => !p.IsApproved);

            return View(unapprovedPodcasts);
        }

        // Approve a specific podcast
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApprovePodcast(int id)
        {
            await _podcastService.ApprovePodcastAsync(id);
            return RedirectToAction(nameof(ApprovePodcasts));
        }

        // List unapproved episodes
        public async Task<IActionResult> ApproveEpisodes()
        {
            var allEpisodes = await _episodeService.GetAllEpisodesAsync();
            var unapprovedEpisodes = allEpisodes.Where(e => !e.IsApproved).Select(e => new EpisodeApprovalViewModel
            {
                EpisodeID = e.EpisodeID,
                Title = e.Title,
                PodcastTitle = e.Podcast?.Title ?? "Unknown",
                CreatorName = e.Podcast?.Creator?.FullName ?? "Unknown",
                ReleaseDate = e.ReleaseDate,
                Duration = e.Duration,
                AudioFileUURL = e.AudioFileURL,
                IsApproved = e.IsApproved
            });
            return View(unapprovedEpisodes);
        }

        // Approve a specific episode
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveEpisode(int id)
        {
            await _episodeService.ApproveEpisodeAsync(id);
            return RedirectToAction(nameof(ApproveEpisodes));
        }

        // Reject a specific episode
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectEpisode(int id)
        {
            await _episodeService.DeleteEpisodeAsync(id);
            return RedirectToAction(nameof(ApproveEpisodes));
        }
    }
}
