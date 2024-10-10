using System.ComponentModel.DataAnnotations;

namespace StudentTeacherQnAPlatform.Entities.Security
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Institute { get; set; }
        public string IDCardNumber { get; set; }
    }
}
