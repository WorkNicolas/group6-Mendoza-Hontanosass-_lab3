/// <summary>
/// Admin Controller
/// </summary>
/// <remarks>
/// Handles administrative operations: user management, content approval
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>1.0</version>
/// <date>2025-10-26</date>
using group6_Mendoza_Hontanosass__lab3.Data.Repositories;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
using group6_Mendoza_Hontanosass__lab3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace group6_Mendoza_Hontanosass__lab3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPodcastService _podcastService;
        private readonly IEpisodeService _episodeService;
        private readonly UserManager<User> _userManager;

        public AdminController(
            IUserRepository userRepository,
            IPodcastService podcastService,
            IEpisodeService episodeService,
            UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _podcastService = podcastService;
            _episodeService = episodeService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var userViewModels = new List<UserManagementViewModel>();

            foreach (var user in users)
            {
                var podcasts = await _podcastService.GetPodcastsByCreatorAsync(user.Id);
                userViewModels.Add(new UserManagementViewModel
                {
                    UserID = user.Id,
                    Username = user.UserName ?? "",
                    Email = user.Email ?? "",
                    FullName = user.FullName,
                    Role = user.Role,
                    CreatedDate = user.CreatedDate,
                    PodcastCount = podcasts.Count(),
                    IsLocked = await _userManager.IsLockedOutAsync(user)
                });
            }

            return View(userViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
                TempData["Success"] = "User locked successfully!";
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
                TempData["Success"] = "User unlocked successfully!";
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                TempData["Success"] = "User deleted successfully!";
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> ApprovePodcasts()
        {
            var podcasts = await _podcastService.GetAllPodcastsAsync();
            var unapprovedPodcasts = podcasts.Where(p => !p.IsApproved);
            return View(unapprovedPodcasts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApprovePodcast(int id)
        {
            await _podcastService.ApprovePodcastAsync(id);
            TempData["Success"] = "Podcast approved successfully!";
            return RedirectToAction(nameof(ApprovePodcasts));
        }

        public async Task<IActionResult> ApproveEpisodes()
        {
            var episodes = await _episodeService.GetAllEpisodesAsync();
            var unapprovedEpisodes = episodes.Where(e => !e.IsApproved);

            var viewModels = unapprovedEpisodes.Select(e => new EpisodeApprovalViewModel
            {
                EpisodeID = e.EpisodeID,
                Title = e.Title,
                PodcastTitle = e.Podcast?.Title ?? "Unknown",
                CreatorName = e.Podcast?.Creator?.FullName ?? "Unknown",
                ReleaseDate = e.ReleaseDate,
                Duration = e.Duration,
                AudioFileURL = e.AudioFileURL,
                IsApproved = e.IsApproved
            }).ToList();

            return View(viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveEpisode(int id)
        {
            await _episodeService.ApproveEpisodeAsync(id);
            TempData["Success"] = "Episode approved successfully!";
            return RedirectToAction(nameof(ApproveEpisodes));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectEpisode(int id)
        {
            await _episodeService.DeleteEpisodeAsync(id);
            TempData["Success"] = "Episode rejected and deleted!";
            return RedirectToAction(nameof(ApproveEpisodes));
        }
    }
}