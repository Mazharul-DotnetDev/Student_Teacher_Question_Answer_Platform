using StudentTeacherQnAPlatform.Entities;
using StudentTeacherQnAPlatform.Entities.Security;
using StudentTeacherQnAPlatform.ViewModels;

namespace StudentTeacherQnAPlatform.Repositories.IRepository
{
    public interface IQuestionService
    {
        List<Question> GetRecentQuestionsForTeachers();
        List<ModerationQuestionViewModel> GetQuestionsToModerate();
        Task AddQuestionAsync(Question question);
        List<UnansweredQuestionViewModel> GetUnansweredQuestions();
        Task AddAnswerAsync(Answer answer);
        List<QuestionViewModel> GetAllQuestions();
        Task RemoveQuestionAsync(int questionId);
        List<RecentQuestionViewModel> GetRecentQuestions();
        List<UserQuestionViewModel> GetQuestionsByUserId(int userId);
        AnswerQuestionViewModel GetQuestionById(int id);
        List<TeacherAnswerViewModel> GetAnswersByTeacherId(int teacherId);
        Task<QuestionDetailsViewModel> GetQuestionDetailsAsync(int id);
        Task<User> GetUserByIdAsync(int userId);
    }
}
