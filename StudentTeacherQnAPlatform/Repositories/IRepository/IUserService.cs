using StudentTeacherQnAPlatform.Entities.Security;

namespace StudentTeacherQnAPlatform.Repositories.IRepository
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email);
        Task RegisterUserAsync(User user, string password);
    }
}
