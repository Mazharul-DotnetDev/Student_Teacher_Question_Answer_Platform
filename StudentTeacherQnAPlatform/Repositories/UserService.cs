using Microsoft.AspNetCore.Identity;
using StudentTeacherQnAPlatform.Entities.Security;
using StudentTeacherQnAPlatform.Repositories.IRepository;

namespace StudentTeacherQnAPlatform.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            await _userRepository.AddUserAsync(user);
        }
    }
}
