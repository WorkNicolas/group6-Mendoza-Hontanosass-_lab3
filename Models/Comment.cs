/// <summary>
/// Comment Model
/// </summary>
/// <remarks>
/// - Represents a comment on an episode
/// - Stored in DynamoDB
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations;
namespace group6_Mendoza_Hontanosass__lab3.Models
{
    [DynamoDBTable("Comments")]
    public class Comment
    {
        [DynamoDBHashKey]
        [Required]
        public int EpisodeId { get; set; }

        [DynamoDBRangeKey]
        [Required]
        public string CommentID { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public int PodcastID { get;set; }

        [Required]
        public string UserID { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Text { get; set; } = string.Empty;

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public DateTime? EditedTimestamp { get; set; }
        public bool IsEdited { get; set; } = false;
    }
}