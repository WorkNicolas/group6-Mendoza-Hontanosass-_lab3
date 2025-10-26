/// <summary>
/// Comment Repository Interface
/// </summary>
/// <remarks>
/// Defines contract for comment data access operations in DynamoDB
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Data.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByEpisodeIdAsync(int episodeId);
        Task<Comment?> GetByIdAsync(int episodeId, string commentId);
        Task<IEnumerable<Comment>> GetByUserIdAsync(string userId);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment> UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(int episodeId, string commentId);
        Task<int> GetCommentCountByEpisodeIdAsync(int episodeId);

    }
}
