using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Subscription Entity Model
/// </summary>
/// <remarks>
/// - Represent user subscription data
/// - Stored in SQL server
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
namespace group6_Mendoza_Hontanosass__lab3.Models
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriptionID { get; set; }

        [Required]
        public string UserID { get; set; } = string.Empty;

        [Required]
        public int PodcastID { get; set; }

        [Required]
        public DateTime SubscribedDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserID")]
        public virtual User? User { get; set; }

        [ForeignKey("PodcastID")]
        public virtual Podcast? Podcast { get; set; }
    }
}