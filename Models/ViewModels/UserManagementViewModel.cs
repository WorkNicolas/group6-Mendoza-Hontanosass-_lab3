/// <summary>
/// User Management View Model
/// </summary>
/// <remarks>
/// View model for admin user management
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanos</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>

namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class UserManagementViewModel
    {
        public string UserID { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PodcastCount { get; set; }
        public bool IsLocked { get; set; }

    }
}
