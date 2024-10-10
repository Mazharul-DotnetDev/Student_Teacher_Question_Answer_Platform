using StudentTeacherQnAPlatform.Entities;

namespace StudentTeacherQnAPlatform.Repositories.IRepository
{
    public interface IQuestionService
    {
        List<Question> GetRecentQuestionsForTeachers();
        List<Question> GetQuestionsToModerate();
    }
}
