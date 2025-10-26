/// <summary>
/// Analytics Controller
/// </summary>
/// <remarks>
/// Handles analytics dashboard for podcasters and admins
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Services;
namespace group6_Mendoza_Hontanosass__lab3.Controllers
{
    [Authorize(Roles = "Podcaster,Admin")]
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly UserManager<User> _userManager;

        public AnalyticsController(IAnalyticsService analyticsService, UserManager<User> userManager)
        {
            _analyticsService = analyticsService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");

            var model = await _analyticsService.GetDashboardDataAsync(userId, isAdmin);
            return View(model);
        }
    }
}
