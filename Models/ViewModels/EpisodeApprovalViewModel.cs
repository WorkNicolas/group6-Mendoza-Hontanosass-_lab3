/// <summary>
/// Episode Approval View Model
/// </summary>
/// <remarks>
/// View model for admin episode approval
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class EpisodeApprovalViewModel
    {
        public int EpisodeID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PodcastTitle { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public string AudioFileURL { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
    }
}
