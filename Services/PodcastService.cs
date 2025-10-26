/// <summary>
/// Podcast Service Implementation
/// </summary>
/// <remarks>
/// Implements podcast business logic with S3 file handling
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using group6_Mendoza_Hontanosass__lab3.Data.Repositories;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public class PodcastService : IPodcastService
    {
        private readonly IPodcastRepository _podcastRepository;
        private readonly IS3Service _s3Service;
        private readonly IConfiguration _configuration;

        public PodcastService(
            IPodcastRepository podcastRepository,
            IS3Service s3Service,
            IConfiguration configuration)
        {
            _podcastRepository = podcastRepository;
            _s3Service = s3Service;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Podcast>> GetAllPodcastsAsync()
        {
            return await _podcastRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Podcast>> GetApprovedPodcastsAsync()
        {
            return await _podcastRepository.GetApprovedAsync();
        }

        public async Task<IEnumerable<Podcast>> GetPodcastsByCreatorAsync(string creatorId)
        {
            return await _podcastRepository.GetByCreatorIdAsync(creatorId);
        }

        public async Task<Podcast?> GetPodcastByIdAsync(int id)
        {
            return await _podcastRepository.GetByIdAsync(id);
        }

        public async Task<Podcast> CreatePodcastAsync(PodcastViewModel model, string creatorId)
        {
            var podcast = new Podcast
            {
                Title = model.Title,
                Description = model.Description,
                CreatorID = creatorId,
                CreatedDate = DateTime.UtcNow
            };

            // Upload thumbnail if provided
            if (model.ThumbnailFile != null)
            {
                var thumbnailFolder = _configuration["AWS:S3:ThumbnailFolder"] ?? "thumbnails";
                podcast.ThumbnailURL = await _s3Service.UploadFileAsync(model.ThumbnailFile, thumbnailFolder);
            }

            return await _podcastRepository.CreateAsync(podcast);
        }

        public async Task<Podcast> UpdatePodcastAsync(PodcastViewModel model)
        {
            var podcast = await _podcastRepository.GetByIdAsync(model.PodcastID!.Value)
                ?? throw new ArgumentException("Podcast not found");

            podcast.Title = model.Title;
            podcast.Description = model.Description;

            // Upload new thumbnail if provided
            if (model.ThumbnailFile != null)
            {
                // Delete old thumbnail
                if (!string.IsNullOrEmpty(podcast.ThumbnailURL))
                {
                    await _s3Service.DeleteFileAsync(podcast.ThumbnailURL);
                }

                var thumbnailFolder = _configuration["AWS:S3:ThumbnailFolder"] ?? "thumbnails";
                podcast.ThumbnailURL = await _s3Service.UploadFileAsync(model.ThumbnailFile, thumbnailFolder);
            }

            return await _podcastRepository.UpdateAsync(podcast);
        }

        public async Task<bool> DeletePodcastAsync(int id)
        {
            var podcast = await _podcastRepository.GetByIdAsync(id);
            if (podcast == null)
                return false;

            // Delete thumbnail from S3
            if (!string.IsNullOrEmpty(podcast.ThumbnailURL))
            {
                await _s3Service.DeleteFileAsync(podcast.ThumbnailURL);
            }

            return await _podcastRepository.DeleteAsync(id);
        }

        public async Task<bool> ApprovePodcastAsync(int id)
        {
            var podcast = await _podcastRepository.GetByIdAsync(id);
            if (podcast == null)
                return false;

            podcast.IsApproved = true;
            await _podcastRepository.UpdateAsync(podcast);
            return true;
        }

    }
}
