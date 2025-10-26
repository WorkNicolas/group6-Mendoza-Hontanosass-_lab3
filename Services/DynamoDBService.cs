/// <summary>
/// DynamoDB Service Implementation
/// </summary>
/// <remarks>
/// Implements DynamoDB table management operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public class DynamoDBService : IDynamoDBService
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;

        public DynamoDBService(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task<bool> CreateTableIfNotExistsAsync(string tableName)
        {
            if (await TableExistsAsync(tableName))
                return true;

            try
            {
                var request = new CreateTableRequest
                {
                    TableName = tableName,
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement("EpisodeID", KeyType.HASH),
                        new KeySchemaElement("CommentID", KeyType.RANGE)
                    },
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition("EpisodeID", ScalarAttributeType.N),
                        new AttributeDefinition("CommentID", ScalarAttributeType.S)
                    },
                    BillingMode = BillingMode.PAY_PER_REQUEST
                };

                await _dynamoDbClient.CreateTableAsync(request);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> TableExistsAsync(string tableName)
        {
            try
            {
                await _dynamoDbClient.DescribeTableAsync(tableName);
                return true;
            }
            catch (ResourceNotFoundException)
            {
                return false;
            }
        }

    }
}
