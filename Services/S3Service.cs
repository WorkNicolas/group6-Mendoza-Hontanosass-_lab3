/// <summary>
/// S3 Service Implementation
/// </summary>
/// <remarks>
/// Implements AWS S3 file upload, download, and deletion operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly string _bucketName;

        public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _configuration = configuration;
            _bucketName = _configuration["AWS:S3:BucketName"]
                ?? throw new ArgumentNullException("S3 bucket name not configured");
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            // Generate unique file name
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var key = $"{folder}/{fileName}";

            try
            {
                using var stream = file.OpenReadStream();
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = key,
                    BucketName = _bucketName,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                var transferUtility = new TransferUtility(_s3Client);
                await transferUtility.UploadAsync(uploadRequest);

                // Return the public URL
                return $"https://{_bucketName}.s3.amazonaws.com/{key}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading file to S3: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteFileAsync(string fileUrl)
        {
            try
            {
                var key = GetFileKeyFromUrl(fileUrl);
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };

                await _s3Client.DeleteObjectAsync(deleteRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetPresignedUrlAsync(string fileKey, int expirationMinutes = 60)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileKey,
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes)
            };

            return await Task.FromResult(_s3Client.GetPreSignedURL(request));
        }

        public string GetFileKeyFromUrl(string fileUrl)
        {
            // Extract key from S3 URL
            // Example: https://bucket.s3.amazonaws.com/folder/file.mp3 -> folder/file.mp3
            var uri = new Uri(fileUrl);
            return uri.AbsolutePath.TrimStart('/');
        }
    }
}
