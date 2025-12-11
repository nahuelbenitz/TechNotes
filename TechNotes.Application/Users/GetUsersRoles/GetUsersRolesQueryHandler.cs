using MediatR;

namespace TechNotes.Application.Users.GetUsersRoles
{
    public class GetUsersRolesQueryHandler : IQueryHandler<GetUsersRolesQuery, List<string>>
    {
        private readonly IUserService _userService;
        public GetUsersRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<List<string>>> Handle(GetUsersRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _userService.GetUserRolesAsync(request.UserId);
            return Result.Ok(roles);
        }
    }
}
