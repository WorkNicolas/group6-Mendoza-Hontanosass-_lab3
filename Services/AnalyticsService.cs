/// <summary>
/// Analytics Service Implementation
/// </summary>
/// <remarks>
/// Implements analytics and reporting logic aggregating data from multiple sources
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using group6_Mendoza_Hontanosass__lab3.Data.Repositories;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IPodcastRepository _podcastRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly ICommentRepository _commentRepository;

        public AnalyticsService(
            IPodcastRepository podcastRepository,
            IEpisodeRepository episodeRepository,
            ICommentRepository commentRepository)
        {
            _podcastRepository = podcastRepository;
            _episodeRepository = episodeRepository;
            _commentRepository = commentRepository;
        }

        public async Task<AnalyticsViewModel> GetDashboardDataAsync(string? userId = null, bool isAdmin = false)
        {
            var podcasts = isAdmin
                ? await _podcastRepository.GetAllAsync()
                : !string.IsNullOrEmpty(userId)
                    ? await _podcastRepository.GetByCreatorIdAsync(userId)
                    : await _podcastRepository.GetApprovedAsync();

            var episodes = await _episodeRepository.GetAllAsync();
            var totalViews = episodes.Sum(e => e.Views);

            // Get comment count (this is expensive - consider caching)
            var totalComments = 0;
            foreach (var episode in episodes)
            {
                totalComments += await _commentRepository.GetCommentCountByEpisodeIdAsync(episode.EpisodeID);
            }

            var topEpisodes = await GetTopEpisodesByViewsAsync(10);
            var podcastStats = await GetPodcastStatisticsAsync();

            return new AnalyticsViewModel
            {
                TotalPodcasts = podcasts.Count(),
                TotalEpisodes = episodes.Count(),
                TotalViews = totalViews,
                TotalComments = totalComments,
                TopEpisodes = topEpisodes,
                PodcastStatistics = podcastStats
            };
        }

        public async Task<List<TopEpisode>> GetTopEpisodesByViewsAsync(int count = 10)
        {
            var episodes = await _episodeRepository.GetApprovedAsync();
            var topEpisodes = new List<TopEpisode>();

            foreach (var episode in episodes.OrderByDescending(e => e.Views).Take(count))
            {
                var commentCount = await _commentRepository.GetCommentCountByEpisodeIdAsync(episode.EpisodeID);

                topEpisodes.Add(new TopEpisode
                {
                    EpisodeID = episode.EpisodeID,
                    Title = episode.Title,
                    PodcastTitle = episode.Podcast?.Title ?? "Unknown",
                    Views = episode.Views,
                    PlayCount = episode.PlayCount,
                    CommentCount = commentCount
                });
            }

            return topEpisodes;
        }

        public async Task<List<PodcastStats>> GetPodcastStatisticsAsync()
        {
            var podcasts = await _podcastRepository.GetApprovedAsync();
            var podcastStats = new List<PodcastStats>();

            foreach (var podcast in podcasts)
            {
                var podcastWithEpisodes = await _podcastRepository.GetByIdWithEpisodesAsync(podcast.PodcastID);
                var subscriberCount = await _podcastRepository.GetSubscriberCountAsync(podcast.PodcastID);
                var totalViews = podcastWithEpisodes?.Episodes.Sum(e => e.Views) ?? 0;

                podcastStats.Add(new PodcastStats
                {
                    PodcastID = podcast.PodcastID,
                    Title = podcast.Title,
                    EpisodeCount = podcastWithEpisodes?.Episodes.Count ?? 0,
                    TotalViews = totalViews,
                    SubscriberCount = subscriberCount
                });
            }

            return podcastStats.OrderByDescending(p => p.TotalViews).ToList();

        }
    }
