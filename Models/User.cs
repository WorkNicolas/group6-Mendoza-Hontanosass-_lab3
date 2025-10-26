/// <summary>
/// User Entity Model
/// </summary>
/// <remarks>
/// - AspNetCore.Identity gives: Authentication, Password Hashing, Role Management, etc.
/// - Auto includes props like: Id, Email, Username, and PasswordHash
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosass</author>
/// <version>0.1</version>
/// <date>2025-10-24</date>
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace group6_Mendoza_Hontanosass__lab3.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Podcast>? Podcasts { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }
    }
}