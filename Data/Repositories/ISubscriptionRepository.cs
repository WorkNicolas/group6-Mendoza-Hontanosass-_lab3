/// <summary>
/// Subscription Repository Interface
/// </summary>
/// <remarks>
/// Defines contract for subscription data access operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Data.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Subscription>> GetByPodcastIdAsync(int podcastId);
        Task<Subscription?> GetByUserAndPodcastAsync(string userId, int podcastId);
        Task<bool> IsSubscribedAsync(string userId, int podcastId);
        Task<Subscription> CreateAsync(Subscription subscription);
        Task<bool> DeleteAsync(int subscriptionId);
        Task<bool> DeleteByUserAndPodcastAsync(string userId, int podcastId);
        Task<int> GetSubscriptionCountAsync(int podcastId);
    }
}
