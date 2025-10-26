/// <summary>
/// Episode View Model
/// </summary>
/// <remarks>
/// View model for creating and editing episodes
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2001-10-25</date>
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class EpisodeViewModel
    {
        public int? EpisodeID { get; set; }

        [Required]
        public int PodcastID { get; set; }

        [Display(Name = "Podcast")]
        public string? PodcastTitle { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Episode Title")]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        [Required]
        [Range(1, 999)]
        [Display(Name = "Duration (minutes)")]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Audio/Video File")]
        public IFormFile? AudioFile { get; set; }
        public string? ExistingAudioFileURL { get; set; }

        [Display(Name = "Thumbnail Image")]
        public IFormFile? ThumbnailFile { get; set; }
        public string? ExistingThumbnailURL { get; set; }
    }
}
