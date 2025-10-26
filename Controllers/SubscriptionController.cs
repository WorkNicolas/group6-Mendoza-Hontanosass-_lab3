/// <summary>
/// Subscription Controller
/// </summary>
/// <remarks>
/// Handles podcast subscription operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>1.0</version>
/// <date>2025-10-26</date>
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using group6_Mendoza_Hontanosass__lab3.Data.Repositories;
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Controllers
{
    [Authorize(Roles = "Listener,Podcaster,Admin")]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly UserManager<User> _userManager;

        public SubscriptionController(
            ISubscriptionRepository subscriptionRepository,
            UserManager<User> userManager)
        {
            _subscriptionRepository = subscriptionRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> MySubscriptions()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var subscriptions = await _subscriptionRepository.GetByUserIdAsync(userId);
            return View(subscriptions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(int podcastId, string returnUrl)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (!await _subscriptionRepository.IsSubscribedAsync(userId, podcastId))
            {
                var subscription = new Subscription
                {
                    UserID = userId,
                    PodcastID = podcastId,
                    SubscribedDate = DateTime.UtcNow
                };

                await _subscriptionRepository.CreateAsync(subscription);
                TempData["Success"] = "Subscribed successfully!";
            }

            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unsubscribe(int podcastId, string returnUrl)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _subscriptionRepository.DeleteByUserAndPodcastAsync(userId, podcastId);
            TempData["Success"] = "Unsubscribed successfully!";
            return Redirect(returnUrl ?? "/");
        }

    }
}
