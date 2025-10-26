/// <summary>
/// Podcast Repository Interface
/// </summary>
/// <remarks>
/// Defines contract for podcast data access operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Data.Repositories
{
    public interface IPodcastRepository
    {
        Task<IEnumerable<Podcast>> GetAllAsync();
        Task<IEnumerable<Podcast>> GetApprovedAsync();
        Task<IEnumerable<Podcast>> GetByCreatorIdAsync(string creatorId);
        Task<Podcast?> GetByIdAsync(int id);
        Task<Podcast?> GetByIdWithEpisodesAsync(int id);
        Task<Podcast> CreateAsync(Podcast podcast);
        Task<Podcast> UpdateAsync(Podcast podcast);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetSubscriberCountAsync(int podcastId);
    }
}
