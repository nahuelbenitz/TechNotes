
namespace TechNotes.Application.Users.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommandHandler : ICommandHandler<RemoveRoleFromUserCommand>
    {
        private readonly IUserService _userService;

        public RemoveRoleFromUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.RemoveToleFromUserAsync(request.UserId, request.RolaName);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error al eliminar rol del usuario: {ex.Message} ");
            }
        }
    }
}
