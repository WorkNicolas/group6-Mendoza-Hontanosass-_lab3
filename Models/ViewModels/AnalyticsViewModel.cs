/// <summary>
/// Analytics View Model
/// </summary>
/// <remarks>
/// View model for displaying analytics dashboard
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class AnalyticsViewModel
    {
        public int TotalPodcasts { get; set; }
        public int TotalEpisodes { get; set; }
        public int TotalViews { get; set; }
        public int TotalComments { get; set; }
        public List<TopEpisode> TopEpisodes { get; set; } = new List<TopEpisode>();
        public List<PodcastStats> PodcastStatistics { get; set; } = new List<PodcastStats>();
    }

    public class TopEpisode
    {
        public int EpisodeID { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Views { get; set; }
        public int PlayCount { get; set; }
        public int CommentCount { get; set; }
        public string? PodcastTitle { get; set; }  // Changed: Made nullable with ?
    }

    public class PodcastStats
    {
        public int PodcastID { get; set; }
        public string Title { get; set; } = string.Empty;
        public int EpisodeCount { get; set; }
        public int TotalViews { get; set; }
        public int SubscriberCount { get; set; }
    }
}