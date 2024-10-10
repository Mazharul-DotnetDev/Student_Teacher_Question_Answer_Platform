using StudentTeacherQnAPlatform.Entities.Data;
using StudentTeacherQnAPlatform.Entities;
using StudentTeacherQnAPlatform.Repositories.IRepository;

namespace StudentTeacherQnAPlatform.Repositories
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Question> GetRecentQuestionsForTeachers()
        {
            return _context.Questions
                .OrderByDescending(q => q.CreatedDate)
                .Take(10)
                .ToList();
        }


        public List<Question> GetQuestionsToModerate()
        {            
            return _context.Questions.Where(q => !q.IsModerated).ToList();
        }

    }
}
