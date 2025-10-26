/// <summary>
/// Subscription Repository Implementation
/// </summary>
/// <remarks>
/// Implements subscription data access operations using Entity Framework Core
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using Microsoft.EntityFrameworkCore;
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Data.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subscription>> GetByUserIdAsync(string userId)
        {
            return await _context.Subscriptions
                .Include(s => s.Podcast)
                .ThenInclude(p => p!.Creator)
                .Where(s => s.UserID == userId)
                .OrderByDescending(s => s.SubscribedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> GetByPodcastIdAsync(int podcastId)
        {
            return await _context.Subscriptions
                .Include(s => s.User)
                .Where(s => s.PodcastID == podcastId)
                .OrderByDescending(s => s.SubscribedDate)
                .ToListAsync();
        }

        public async Task<Subscription?> GetByUserAndPodcastAsync(string userId, int podcastId)
        {
            return await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserID == userId && s.PodcastID == podcastId);
        }

        public async Task<bool> IsSubscribedAsync(string userId, int podcastId)
        {
            return await _context.Subscriptions
                .AnyAsync(s => s.UserID == userId && s.PodcastID == podcastId);
        }

        public async Task<Subscription> CreateAsync(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<bool> DeleteAsync(int subscriptionId)
        {
            var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
            if (subscription == null)
                return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByUserAndPodcastAsync(string userId, int podcastId)
        {
            var subscription = await GetByUserAndPodcastAsync(userId, podcastId);
            if (subscription == null)
                return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetSubscriptionCountAsync(int podcastId)
        {
            return await _context.Subscriptions
                .CountAsync(s => s.PodcastID == podcastId);
        }
    }
}
