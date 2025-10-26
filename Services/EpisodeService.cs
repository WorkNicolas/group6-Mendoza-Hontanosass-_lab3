/// <summary>
/// Episode Service Implementation
/// </summary>
/// <remarks>
/// Implements episode business logic with S3 file handling and comment integration
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using group6_Mendoza_Hontanosass__lab3.Data.Repositories;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;

namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IPodcastRepository _podcastRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IS3Service _s3Service;
        private readonly IConfiguration _configuration;

        public EpisodeService(
            IEpisodeRepository episodeRepository,
            IPodcastRepository podcastRepository,
            ISubscriptionRepository subscriptionRepository,
            ICommentRepository commentRepository,
            IS3Service s3Service,
            IConfiguration configuration)
        {
            _episodeRepository = episodeRepository;
            _podcastRepository = podcastRepository;
            _subscriptionRepository = subscriptionRepository;
            _commentRepository = commentRepository;
            _s3Service = s3Service;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Episode>> GetAllEpisodesAsync()
        {
            return await _episodeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Episode>> GetApprovedEpisodesAsync()
        {
            return await _episodeRepository.GetApprovedAsync();
        }

        public async Task<IEnumerable<Episode>> GetEpisodesByPodcastAsync(int podcastId)
        {
            return await _episodeRepository.GetByPodcastIdAsync(podcastId);
        }

        public async Task<Episode?> GetEpisodeByIdAsync(int id)
        {
            return await _episodeRepository.GetByIdAsync(id);
        }

        public async Task<EpisodeDetailsViewModel> GetEpisodeDetailsAsync(int id, string? userId)
        {
            var episode = await _episodeRepository.GetByIdWithPodcastAsync(id)
                ?? throw new ArgumentException("Episode not found");

            var comments = await _commentRepository.GetByEpisodeIdAsync(id);
            var isSubscribed = !string.IsNullOrEmpty(userId)
                && await _subscriptionRepository.IsSubscribedAsync(userId, episode.PodcastID);

            return new EpisodeDetailsViewModel
            {
                Episode = episode,
                Podcast = episode.Podcast!,
                Comments = comments.ToList(),
                IsSubscribed = isSubscribed,
                CurrentUserId = userId
            };
        }

        public async Task<Episode> CreateEpisodeAsync(EpisodeViewModel model)
        {
            var episode = new Episode
            {
                PodcastID = model.PodcastID,
                Title = model.Title,
                Description = model.Description ?? string.Empty,
                ReleaseDate = model.ReleaseDate,
                Duration = model.Duration
            };

            // Upload audio file
            if (model.AudioFile != null)
            {
                var audioFolder = _configuration["AWS:S3:AudioFolder"] ?? "audio";
                episode.AudioFileURL = await _s3Service.UploadFileAsync(model.AudioFile, audioFolder);
            }
            else
            {
                throw new ArgumentException("Audio file is required");
            }

            // Upload thumbnail if provided
            if (model.ThumbnailFile != null)
            {
                var thumbnailFolder = _configuration["AWS:S3:ThumbnailFolder"] ?? "thumbnails";
                var thumbnailUrl = await _s3Service.UploadFileAsync(model.ThumbnailFile, thumbnailFolder);
                episode.ThumbnailURL = thumbnailUrl ?? string.Empty;  // Fixed: Handle null with ?? operator
            }

            return await _episodeRepository.CreateAsync(episode);
        }

        public async Task<Episode> UpdateEpisodeAsync(EpisodeViewModel model)
        {
            var episode = await _episodeRepository.GetByIdAsync(model.EpisodeID!.Value)
                ?? throw new ArgumentException("Episode not found");

            episode.Title = model.Title;
            episode.Description = model.Description ?? string.Empty;
            episode.ReleaseDate = model.ReleaseDate;
            episode.Duration = model.Duration;

            // Upload new audio file if provided
            if (model.AudioFile != null)
            {
                // Delete old file
                if (!string.IsNullOrEmpty(episode.AudioFileURL))
                {
                    await _s3Service.DeleteFileAsync(episode.AudioFileURL);
                }

                var audioFolder = _configuration["AWS:S3:AudioFolder"] ?? "audio";
                episode.AudioFileURL = await _s3Service.UploadFileAsync(model.AudioFile, audioFolder);
            }

            // Upload new thumbnail if provided
            if (model.ThumbnailFile != null)
            {
                // Delete old thumbnail
                if (!string.IsNullOrEmpty(episode.ThumbnailURL))
                {
                    await _s3Service.DeleteFileAsync(episode.ThumbnailURL);
                }

                var thumbnailFolder = _configuration["AWS:S3:ThumbnailFolder"] ?? "thumbnails";
                var thumbnailUrl = await _s3Service.UploadFileAsync(model.ThumbnailFile, thumbnailFolder);
                episode.ThumbnailURL = thumbnailUrl ?? string.Empty;  // Fixed: Handle null with ?? operator
            }

            return await _episodeRepository.UpdateAsync(episode);
        }

        public async Task<bool> DeleteEpisodeAsync(int id)
        {
            var episode = await _episodeRepository.GetByIdAsync(id);
            if (episode == null)
                return false;

            // Delete audio file from S3
            if (!string.IsNullOrEmpty(episode.AudioFileURL))
            {
                await _s3Service.DeleteFileAsync(episode.AudioFileURL);
            }

            // Delete thumbnail from S3
            if (!string.IsNullOrEmpty(episode.ThumbnailURL))
            {
                await _s3Service.DeleteFileAsync(episode.ThumbnailURL);
            }

            return await _episodeRepository.DeleteAsync(id);
        }

        public async Task<bool> ApproveEpisodeAsync(int id)
        {
            var episode = await _episodeRepository.GetByIdAsync(id);
            if (episode == null)
                return false;

            episode.IsApproved = true;
            await _episodeRepository.UpdateAsync(episode);
            return true;
        }

        public async Task IncrementViewAsync(int episodeId)
        {
            await _episodeRepository.IncrementViewsAsync(episodeId);
        }

        public async Task IncrementPlayCountAsync(int episodeId)
        {
            await _episodeRepository.IncrementPlayCountAsync(episodeId);
        }

        public async Task<IEnumerable<Episode>> SearchEpisodesAsync(EpisodeSearchViewModel model)
        {
            return await _episodeRepository.SearchAsync(
                model.SearchTerm,
                model.PodcastID,
                model.FromDate,
                model.ToDate,
                model.SortBy
            );
        }
    }
}