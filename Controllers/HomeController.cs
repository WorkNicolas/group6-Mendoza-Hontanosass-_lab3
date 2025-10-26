
/// <summary>
/// Home Controller
/// </summary>
/// <remarks>
/// Handles landing page, about, and general navigation
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>

using System.Diagnostics;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Services;
using Microsoft.AspNetCore.Mvc;

namespace group6_Mendoza_Hontanosass__lab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPodcastService _podcastService;
        private readonly IEpisodeService _episodeService;

        public HomeController(IPodcastService podcastService, IEpisodeService episodeService)
        {
            _podcastService = podcastService;
            _episodeService = episodeService;
        }

        public async Task<IActionResult> Index()
        {
            var podcasts = await _podcastService.GetApprovedPodcastsAsync();
            var episodes = await _episodeService.GetApprovedEpisodesAsync();

            ViewBag.Podcasts = podcasts.Take(6);
            ViewBag.RecentEpisodes = episodes.Take(10);

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

    }
}
