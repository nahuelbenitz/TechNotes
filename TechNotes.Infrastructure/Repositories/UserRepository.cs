using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechNotes.Domain.User;
using TechNotes.Infrastructure.Users;

namespace TechNotes.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<IUser>> GetAllUserAsync()
        { 
            return await _userManager.Users.Select(u => (IUser)u).ToListAsync();
        }

        public async Task<IUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
