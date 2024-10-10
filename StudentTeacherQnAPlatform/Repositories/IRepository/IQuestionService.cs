using StudentTeacherQnAPlatform.Entities;

namespace StudentTeacherQnAPlatform.Repositories.IRepository
{
    public interface IQuestionService
    {
        List<Question> GetRecentQuestionsForTeachers();
        List<Question> GetQuestionsToModerate();
        Task AddQuestionAsync(Question question);
        List<Question> GetUnansweredQuestions();
        Task AddAnswerAsync(Answer answer);
        List<Question> GetAllQuestions();
        Task RemoveQuestionAsync(int questionId);
        List<Question> GetRecentQuestions();
        List<Question> GetQuestionsByUserId(int userId);
        Question GetQuestionById(int id);
        List<Answer> GetAnswersByTeacherId(int teacherId);
        Task<Question> GetQuestionDetailsAsync(int id);
    }
}
