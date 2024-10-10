using System.ComponentModel.DataAnnotations;

namespace StudentTeacherQnAPlatform.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int RoleId { get; set; }
        public string Institute { get; set; }
        public string IDCardNumber { get; set; }
    }
}
