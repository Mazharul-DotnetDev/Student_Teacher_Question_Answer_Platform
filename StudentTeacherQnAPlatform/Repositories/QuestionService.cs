using StudentTeacherQnAPlatform.Entities.Data;
using StudentTeacherQnAPlatform.Entities;
using StudentTeacherQnAPlatform.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using StudentTeacherQnAPlatform.Entities.Security;
using StudentTeacherQnAPlatform.ViewModels;

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

        public List<ModerationQuestionViewModel> GetQuestionsToModerate()
        {
            return _context.Questions
                .Where(q => !q.IsModerated)
                .Include(q => q.User)
                .Select(q => new ModerationQuestionViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    Content = q.Content,
                    CreatedDate = q.CreatedDate,
                    UserName = q.User != null ? q.User.Name : "Unknown"
                })
                .ToList();
        }


        public async Task AddQuestionAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public List<UserQuestionViewModel> GetQuestionsByUserId(int userId)
        {
            return _context.Questions
                .Where(q => q.UserId == userId)
                .Select(q => new UserQuestionViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    Content = q.Content,
                    CreatedDate = q.CreatedDate
                })
                .ToList();
        }

        public List<UnansweredQuestionViewModel> GetUnansweredQuestions()
        {
            return _context.Questions
                .Where(q => !q.Answers.Any())
                .Select(q => new UnansweredQuestionViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    Content = q.Content,
                    CreatedDate = q.CreatedDate,
                    UserName = q.User != null ? q.User.Name : "Unknown"
                }).ToList();
        }


        public async Task AddAnswerAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync();
        }

        public List<TeacherAnswerViewModel> GetAnswersByTeacherId(int teacherId)
        {
            return _context.Answers
                .Where(a => a.TeacherId == teacherId)
                .Include(a => a.Question) // Include the question to get the title
                .Select(a => new TeacherAnswerViewModel
                {
                    AnswerId = a.Id,
                    AnswerContent = a.Content,
                    CreatedDate = a.CreatedDate,
                    QuestionTitle = a.Question != null ? a.Question.Title : "Unknown Question"
                }).ToList();
        }

        public List<QuestionViewModel> GetAllQuestions()
        {
            return _context.Questions.Include(q => q.User)
                .Select(q => new QuestionViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    Content = q.Content,
                    CreatedDate = q.CreatedDate,
                    UserName = q.User != null ? q.User.Name : "Unknown"
                }).ToList();
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

        public List<RecentQuestionViewModel> GetRecentQuestions()
        {
            return _context.Questions
                .OrderByDescending(q => q.CreatedDate)
                .Take(10)
                .Select(q => new RecentQuestionViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    Content = q.Content,
                    CreatedDate = q.CreatedDate
                })
                .ToList();
        }

        public AnswerQuestionViewModel GetQuestionById(int id)
        {
            var question = _context.Questions.FirstOrDefault(q => q.Id == id);
            if (question == null)
            {
                return null;
            }

            return new AnswerQuestionViewModel
            {
                QuestionId = question.Id,
                Title = question.Title,
                Content = question.Content
            };
        }

        public async Task<QuestionDetailsViewModel> GetQuestionDetailsAsync(int id)
        {
            var question = await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                    .ThenInclude(a => a.Teacher)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
            {
                return null;
            }
            
            return new QuestionDetailsViewModel
            {
                Id = question.Id,
                Title = question.Title,
                Content = question.Content,
                CreatedDate = question.CreatedDate,
                UserName = question.User?.Name ?? "Unknown",
                Answers = question.Answers.Select(a => new AnswerViewModel
                {
                    Id = a.Id,
                    Content = a.Content,
                    CreatedDate = a.CreatedDate,
                    TeacherName = a.Teacher?.Name ?? "Unknown"
                }).ToList()
            };
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
