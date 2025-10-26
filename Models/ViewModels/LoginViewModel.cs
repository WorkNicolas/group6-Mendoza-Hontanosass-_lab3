/// <summary>
/// Login View Model
/// </summary>
/// <remarks>
/// View model for user login form
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-25</date>
using System.ComponentModel.DataAnnotations;
namespace group6_Mendoza_Hontanosass__lab3.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email or Username")]
        public string EmailOrUsername { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
