/// <summary>
/// DynamoDB Service Interface
/// </summary>
/// <remarks>
/// Defines contract for DynamoDB helper operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public interface IDynamoDBService
    {
        Task<bool> CreateTableIfNotExistsAsync(string tableName);
        Task<bool> TableExistsAsync(string tableName);

    }
}
