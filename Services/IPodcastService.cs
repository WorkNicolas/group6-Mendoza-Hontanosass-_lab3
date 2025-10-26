/// <summary>
/// Podcast Service Interface
/// </summary>
/// <remarks>
/// Defines contract for podcast business logic
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public interface IPodcastService
    {
        Task<IEnumerable<Podcast>> GetAllPodcastsAsync();
        Task<IEnumerable<Podcast>> GetApprovedPodcastsAsync();
        Task<IEnumerable<Podcast>> GetPodcastsByCreatorAsync(string creatorId);
        Task<Podcast?> GetPodcastByIdAsync(int id);
        Task<Podcast> CreatePodcastAsync(PodcastViewModel model, string creatorId);
        Task<Podcast> UpdatePodcastAsync(PodcastViewModel model);
        Task<bool> DeletePodcastAsync(int id);
        Task<bool> ApprovePodcastAsync(int id);

    }
}
