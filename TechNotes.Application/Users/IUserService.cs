namespace TechNotes.Application.Users
{
    public interface IUserService
    {
        Task<string> GetCurrentUserIdAsync();
        Task<bool> IsCurrentUserInRoleAsync(string role);
        Task<bool> CurrentUserCanCreateNoteAsync();
        Task<bool> CurrentUserCanUpdateNoteAsync(int noteId);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task AddUseRoleAsync(string userId, string roleName);
        Task RemoveToleFromUserAsync(string userId, string roleName);
    }
}
