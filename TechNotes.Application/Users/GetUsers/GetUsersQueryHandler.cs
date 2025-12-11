using MediatR;
using TechNotes.Domain.User;

namespace TechNotes.Application.Users.GetUsers
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetUsersQueryHandler(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<Result<List<UserResponse>>> Handle (GetUsersQuery request, CancellationToken cancellationToken)
        {
            if (!await _userService.IsCurrentUserInRoleAsync("Admin"))
            {
                return Result.Fail<List<UserResponse>>("Unauthorized access. Admin role required.");
            }

            var users = await _userRepository.GetAllUserAsync();
            var response = new List<UserResponse>();
            foreach (var user in users)
            {
                var roles = await _userService.GetUserRolesAsync(user.Id);
                var userResponse = user.Adapt<UserResponse>();
                userResponse.roles = string.Join(", ", roles);
                response.Add(userResponse);
            }

            return response;
        }
    }
}
