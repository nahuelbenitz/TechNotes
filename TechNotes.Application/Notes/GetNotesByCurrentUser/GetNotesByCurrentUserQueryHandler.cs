
using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.GetNotesByCurrentUser
{
    public class GetNotesByCurrentUserQueryHandler : IQueryHandler<GetNotesByCurrentUserQuery, List<NoteResponse>>
    {
        private readonly INoteRepository _repository;
        private readonly IUserService _userService;

        public GetNotesByCurrentUserQueryHandler(INoteRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<Result<List<NoteResponse>>> Handle(GetNotesByCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUserIdAsync();
            var note = await _repository.GetNotesByUserAsync(userId);

            var result = note.Adapt<List<NoteResponse>>();
            return result.OrderByDescending(a => a.PublishedAt).ToList();
        }
    }
}
