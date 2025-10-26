/// <summary>
/// Podcast View Model
/// </summary>
/// <remarks>
/// View model for creating and editing podcasts
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class PodcastViewModel
    {
        public int? PodcastID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Podcast Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Thumbnail Image")]
        public IFormFile? ThumbnailFile { get; set; }

        public string? ExistingThumbnailURL { get; set; }
    }
}
