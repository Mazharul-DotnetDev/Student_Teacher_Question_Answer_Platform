using StudentTeacherQnAPlatform.Entities.Security;
using System.ComponentModel.DataAnnotations;

namespace StudentTeacherQnAPlatform.Entities
{
    public class Moderator
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
