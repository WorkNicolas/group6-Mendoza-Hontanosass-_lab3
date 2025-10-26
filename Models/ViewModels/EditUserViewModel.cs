using System.ComponentModel.DataAnnotations;

namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string UserId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Role")]
        public UserRole Role { get; set; }

    }
}
