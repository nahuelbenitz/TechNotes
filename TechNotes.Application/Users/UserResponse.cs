namespace TechNotes.Application.Users
{
    public record struct UserResponse(string Id, string Username, string Email, string roles)
    {
    }
}
