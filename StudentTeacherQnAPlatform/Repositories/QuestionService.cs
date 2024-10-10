using StudentTeacherQnAPlatform.Entities.Data;
using StudentTeacherQnAPlatform.Entities;
using StudentTeacherQnAPlatform.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddQuestionAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public List<Question> GetQuestionsByUserId(int userId)
        {
            return _context.Questions.Where(q => q.UserId == userId).ToList();
        }

        public List<Question> GetUnansweredQuestions()
        {
            return _context.Questions.Where(q => !q.Answers.Any()).ToList();
        }
        public async Task AddAnswerAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync();
        }

        public List<Answer> GetAnswersByTeacherId(int teacherId)
        {
            return _context.Answers.Where(a => a.TeacherId == teacherId).ToList();
        }

        public List<Question> GetAllQuestions()
        {
            //return _context.Questions.ToList();
            return _context.Questions.Include(q => q.User).ToList();
        }

        public async Task RemoveQuestionAsync(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public List<Question> GetRecentQuestions()
        {
            return _context.Questions.OrderByDescending(q => q.CreatedDate).Take(10).ToList();
        }

        public Question GetQuestionById(int id)
        {
            return _context.Questions.FirstOrDefault(q => q.Id == id);
        }

        public async Task<Question> GetQuestionDetailsAsync(int id)
        {
            return await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                    .ThenInclude(a => a.Teacher)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
