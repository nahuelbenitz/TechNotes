namespace TechNotes.Application.Users.GetUsersRoles
{
    public class GetUsersRolesQuery : IQuery<List<string>>
    {
        public required string UserId { get; set; }
    }
}
