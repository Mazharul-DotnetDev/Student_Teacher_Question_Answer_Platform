using StudentTeacherQnAPlatform.Entities.Security;
using System.ComponentModel.DataAnnotations;

namespace StudentTeacherQnAPlatform.Entities
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int TeacherId { get; set; }
        public User Teacher { get; set; }
    }
}
