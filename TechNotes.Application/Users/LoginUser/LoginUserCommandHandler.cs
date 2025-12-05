
using TechNotes.Application.Authentication;

namespace TechNotes.Application.Users.LoginUser
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var succeess = await _authenticationService.LoginUserAsync(request.UserName, request.Password);

            return succeess
                ? Result.Ok()
                : Result.Fail("Invalid username or password.");
        }
    }
}
