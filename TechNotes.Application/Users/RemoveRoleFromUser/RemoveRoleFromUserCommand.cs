namespace TechNotes.Application.Users.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommand : ICommand
    {
        public required string UserId { get; set; }
        public required string RolaName { get; set; }
    }
}
