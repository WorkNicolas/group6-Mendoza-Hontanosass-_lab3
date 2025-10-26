/// <summary>
/// S3 Service Interface
/// </summary>
/// <remarks>
/// Defines contract for AWS S3 file operations
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using Microsoft.AspNetCore.Http;
namespace group6_Mendoza_Hontanosass__lab3.Services
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
        Task<bool> DeleteFileAsync(string fileUrl);
        Task<string> GetPresignedUrlAsync(string fileKey, int expirationMinutes = 60);
        string GetFileKeyFromUrl(string fileUrl);
    }
}
