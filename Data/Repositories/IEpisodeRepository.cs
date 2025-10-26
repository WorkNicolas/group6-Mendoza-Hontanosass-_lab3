/// <summary>
/// Episode Repository Interface
/// </summary>
/// <remarks>
/// Defines contract for episode data access operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Data.Repositories
{
    public interface IEpisodeRepository
    {
        Task<IEnumerable<Episode>> GetAllAsync();
        Task<IEnumerable<Episode>> GetApprovedAsync();
        Task<IEnumerable<Episode>> GetByPodcastIdAsync(int podcastId);
        Task<Episode?> GetByIdAsync(int id);
        Task<Episode?> GetByIdWithPodcastAsync(int id);
        Task<Episode> CreateAsync(Episode episode);
        Task<Episode> UpdateAsync(Episode episode);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task IncrementViewsAsync(int episodeId);
        Task IncrementPlayCountAsync(int episodeId);
        Task<IEnumerable<Episode>> SearchAsync(string? searchTerm, int? podcastId, DateTime? fromDate, DateTime? toDate, string sortBy);
    }
}
