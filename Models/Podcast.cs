/// <summary>
/// Podcast Entity Model
/// </summary>
/// <remarks>
/// - Stored in SQL server
/// - Represent podcast with 
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosass</author>
/// <version>0.1</version>
/// <date>2025-10-24</date>
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace group6_Mendoza_Hontanosass__lab3.Models
{
    public class Podcast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PodcastID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string CreatorID { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string? ThumbnailURL { get; set; }
        public bool IsApproved { get; set; } = false;

        // Navigation Properties
        [ForeignKey("CreatorID")]
        public virtual User? Creator { get; set; }
        public virtual ICollection<Episode>? Episodes { get; set; }
        public virtual ICollection<Subscription>? Subscriptions { get; set; }
    }
}
