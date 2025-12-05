
using TechNotes.Application.Authentication;

namespace TechNotes.Application.Users.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var success = await _authenticationService.RegisterUserAsync(
                request.UserName,
                request.Email,
                request.Password);

            if (success.Succeeded)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail(string.Join(", ", success.Errors));
            }
        }
    }
}
