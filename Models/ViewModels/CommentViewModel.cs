/// <summary>
/// Comment View Model
/// </summary>
/// <remarks>
/// View model for creating and editing comments
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using System.ComponentModel.DataAnnotations;
namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class CommentViewModel
    {
        public string? CommentID { get; set; }
        
        [Required]
        public int EpisodeID { get; set; }

        [Required]
        public int PodcastID { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 1)]
        [Display(Name = "Comment")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; } = string.Empty;
    }
}
