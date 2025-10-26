using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;

/// <summary>
/// Analytics Service Interface
/// </summary>
/// <remarks>
/// Defines contract for analytics and reporting
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public interface IAnalyticsService
    {
        Task<AnalyticsViewModel> GetDashboardDataAsync(string? userId = null, bool isAdmin = false);
        Task<List<TopEpisode>> GetTopEpisodesByViewsAsync(int count = 10);
        Task<List<PodcastStats>> GetPodcastStatisticsAsync();

    }
}
