/// <summary>
/// Podcast Repository Implementation
/// </summary>
/// <remarks>
/// Implements podcast data access operations using Entity Framework Core
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using Microsoft.EntityFrameworkCore;
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Data.Repositories
{
    public class PodcastRepository : IPodcastRepository
    {
        private readonly ApplicationDbContext _context;

        public PodcastRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Podcast>> GetAllAsync()
        {
            return await _context.Podcasts
                .Include(p => p.Creator)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Podcast>> GetApprovedAsync()
        {
            return await _context.Podcasts
                .Include(p => p.Creator)
                .Where(p => p.IsApproved)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Podcast>> GetByCreatorIdAsync(string creatorId)
        {
            return await _context.Podcasts
                .Include(p => p.Creator)
                .Where(p => p.CreatorID == creatorId)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<Podcast?> GetByIdAsync(int id)
        {
            return await _context.Podcasts
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(p => p.PodcastID == id);
        }

        public async Task<Podcast?> GetByIdWithEpisodesAsync(int id)
        {
            return await _context.Podcasts
                .Include(p => p.Creator)
                .Include(p => p.Episodes)
                .FirstOrDefaultAsync(p => p.PodcastID == id);
        }

        public async Task<Podcast> CreateAsync(Podcast podcast)
        {
            _context.Podcasts.Add(podcast);
            await _context.SaveChangesAsync();
            return podcast;
        }

        public async Task<Podcast> UpdateAsync(Podcast podcast)
        {
            _context.Entry(podcast).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return podcast;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var podcast = await _context.Podcasts.FindAsync(id);
            if (podcast == null)
                return false;

            _context.Podcasts.Remove(podcast);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Podcasts.AnyAsync(p => p.PodcastID == id);
        }

        public async Task<int> GetSubscriberCountAsync(int podcastId)
        {
            return await _context.Subscriptions
                .CountAsync(s => s.PodcastID == podcastId);
        }
    }
}
