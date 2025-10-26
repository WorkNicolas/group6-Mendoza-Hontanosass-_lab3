/// <summary>
/// Episode Details View Model
/// </summary>
/// <remarks>
/// View model for displaying episode details with comments
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class EpisodeDetailsViewModel
    {
        public Episode Episode { get; set; } = new Episode();
        public Podcast Podcast { get; set; } = new Podcast();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public bool IsSubscribed { get; set; }
        public string? CurrentUserId { get; set; }
    }
}
