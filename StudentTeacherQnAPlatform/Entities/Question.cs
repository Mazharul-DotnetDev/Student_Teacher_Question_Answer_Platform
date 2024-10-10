using StudentTeacherQnAPlatform.Entities.Security;
using System.ComponentModel.DataAnnotations;

namespace StudentTeacherQnAPlatform.Entities
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsModerated { get; set; } = false;

        public int UserId { get; set; }

        public User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
