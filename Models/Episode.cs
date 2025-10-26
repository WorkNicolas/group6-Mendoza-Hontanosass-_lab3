/// <summary>
/// Episode Entity Model
/// </summary>
/// <remarks>
/// - Podcast episode with metadata
/// - Stored in SQL server
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosass</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace group6_Mendoza_Hontanosass__lab3.Models
{
    public class Episode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EpisodeID { get; set; }

        [Required]
        public int PodcastID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Duration { get; set; } // mins

        [Required]
        [Range(0, int.MaxValue)]
        public int PlayCount { get; set; } = 0;

        [Required]
        [Range(0, int.MaxValue)]
        public int Views { get; set; } = 0;

        [Required]
        [StringLength(500)]
        public string AudioFileURL { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ThumbnailURL { get; set; }

        public bool IsApproved { get; set; } = false;

        // Navigation Properties
        [ForeignKey("PodcastID")]
        public virtual Podcast? Podcast { get; set; }
    }
}
