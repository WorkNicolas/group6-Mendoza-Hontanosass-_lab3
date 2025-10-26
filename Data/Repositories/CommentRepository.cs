/// <summary>
/// Comment Repository Implementation
/// </summary>
/// <remarks>
/// Implements comment data access operations using AWS DynamoDB
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using group6_Mendoza_Hontanosass__lab3.Models;
namespace group6_Mendoza_Hontanosass__lab3.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDynamoDBContext _context;

        public CommentRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient); // CS0618: Not null
        }

        public async Task<IEnumerable<Comment>> GetByEpisodeIdAsync(int episodeId)
        {
            var search = _context.QueryAsync<Comment>(
                episodeId,
                QueryOperator.BeginsWith,
                new object[] { "" }
            );

            var comments = await search.GetRemainingAsync();
            return comments.OrderByDescending(c => c.Timestamp);
        }

        public async Task<Comment?> GetByIdAsync(int episodeId, string commentId)
        {
            return await _context.LoadAsync<Comment>(episodeId, commentId);
        }

        public async Task<IEnumerable<Comment>> GetByUserIdAsync(string userId)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "UserID-Index"
            };

            var search = _context.QueryAsync<Comment>(userId, config); // DEPRECATED: idk what to replace this with
            var comments = await search.GetRemainingAsync();
            return comments.OrderByDescending(c => c.Timestamp);
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            if (string.IsNullOrEmpty(comment.CommentID))
            {
                comment.CommentID = Guid.NewGuid().ToString();
            }

            comment.Timestamp = DateTime.UtcNow;
            await _context.SaveAsync(comment);
            return comment;
        }

        public async Task<Comment> UpdateAsync(Comment comment)
        {
            comment.EditedTimestamp = DateTime.UtcNow;
            comment.IsEdited = true;
            await _context.SaveAsync(comment);
            return comment;
        }

        public async Task<bool> DeleteAsync(int episodeId, string commentId)
        {
            try
            {
                await _context.DeleteAsync<Comment>(episodeId, commentId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetCommentCountByEpisodeIdAsync(int episodeId)
        {
            var comments = await GetByEpisodeIdAsync(episodeId);
            return comments.Count();
        }
    }
}
