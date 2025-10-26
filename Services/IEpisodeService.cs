/// <summary>
/// Episode Service Interface
/// </summary>
/// <remarks>
/// Defines contract for episode business logic
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public interface IEpisodeService
    {
        Task<IEnumerable<Episode>> GetAllEpisodesAsync();
        Task<IEnumerable<Episode>> GetApprovedEpisodesAsync();
        Task<IEnumerable<Episode>> GetEpisodesByPodcastAsync(int podcastId);
        Task<Episode?> GetEpisodeByIdAsync(int id);
        Task<EpisodeDetailsViewModel> GetEpisodeDetailsAsync(int id, string? userId);
        Task<Episode> CreateEpisodeAsync(EpisodeViewModel model);
        Task<Episode> UpdateEpisodeAsync(EpisodeViewModel model);
        Task<bool> DeleteEpisodeAsync(int id);
        Task<bool> ApproveEpisodeAsync(int id);
        Task IncrementViewAsync(int episodeId);
        Task IncrementPlayCountAsync(int episodeId);
        Task<IEnumerable<Episode>> SearchEpisodesAsync(EpisodeSearchViewModel model);

    }
}
