/// <summary>
/// Episode Repository Implementation
/// </summary>
/// <remarks>
/// Implements episode data access operations using Entity Framework Core
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using Microsoft.EntityFrameworkCore;
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Data.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly ApplicationDbContext _context;

        public EpisodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Episode>> GetAllAsync()
        {
            return await _context.Episodes
                .Include(e => e.Podcast)
                .OrderByDescending(e => e.ReleaseDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Episode>> GetApprovedAsync()
        {
            return await _context.Episodes
                .Include(e => e.Podcast)
                .Where(e => e.IsApproved && e.Podcast!.IsApproved)
                .OrderByDescending(e => e.ReleaseDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Episode>> GetByPodcastIdAsync(int podcastId)
        {
            return await _context.Episodes
                .Include(e => e.Podcast)
                .Where(e => e.PodcastID == podcastId)
                .OrderByDescending(e => e.ReleaseDate)
                .ToListAsync();
        }

        public async Task<Episode?> GetByIdAsync(int id)
        {
            return await _context.Episodes
                .Include(e => e.Podcast)
                .FirstOrDefaultAsync(e => e.EpisodeID == id);
        }

        public async Task<Episode?> GetByIdWithPodcastAsync(int id)
        {
            return await _context.Episodes
                .Include(e => e.Podcast)
                .ThenInclude(p => p!.Creator)
                .FirstOrDefaultAsync(e => e.EpisodeID == id);
        }

        public async Task<Episode> CreateAsync(Episode episode)
        {
            _context.Episodes.Add(episode);
            await _context.SaveChangesAsync();
            return episode;
        }

        public async Task<Episode> UpdateAsync(Episode episode)
        {
            _context.Entry(episode).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return episode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
                return false;

            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Episodes.AnyAsync(e => e.EpisodeID == id);
        }

        public async Task IncrementViewsAsync(int episodeId)
        {
            var episode = await _context.Episodes.FindAsync(episodeId);
            if (episode != null)
            {
                episode.Views++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task IncrementPlayCountAsync(int episodeId)
        {
            var episode = await _context.Episodes.FindAsync(episodeId);
            if (episode != null)
            {
                episode.PlayCount++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Episode>> SearchAsync(
            string? searchTerm,
            int? podcastId,
            DateTime? fromDate,
            DateTime? toDate,
            string sortBy)
        {
            var query = _context.Episodes
                .Include(e => e.Podcast)
                .Where(e => e.IsApproved && e.Podcast!.IsApproved)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(e =>
                    e.Title.Contains(searchTerm) ||
                    (e.Description != null && e.Description.Contains(searchTerm)) ||
                    e.Podcast!.Title.Contains(searchTerm));
            }

            if (podcastId.HasValue)
            {
                query = query.Where(e => e.PodcastID == podcastId.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(e => e.ReleaseDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(e => e.ReleaseDate <= toDate.Value);
            }

            query = sortBy switch
            {
                "Title" => query.OrderBy(e => e.Title),
                "Views" => query.OrderByDescending(e => e.Views),
                "PlayCount" => query.OrderByDescending(e => e.PlayCount),
                _ => query.OrderByDescending(e => e.ReleaseDate)
            };

            return await query.ToListAsync();
        }
    }
}
