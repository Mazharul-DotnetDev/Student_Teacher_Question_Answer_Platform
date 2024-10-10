using StudentTeacherQnAPlatform.Entities.Security;

namespace StudentTeacherQnAPlatform.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
    }
}
