
using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.TogglePublishNote
{
    public class TogglePublishNoteCommandHandler : ICommandHandler<TogglePublishNoteCommand, NoteResponse>
    {
        private readonly INoteRepository _repository;
        private readonly IUserService _userService;

        public TogglePublishNoteCommandHandler(INoteRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<Result<NoteResponse>> Handle(TogglePublishNoteCommand request, CancellationToken cancellationToken)
        {
            var currentUserCanEdit = await _userService.CurrentUserCanUpdateNoteAsync(request.NoteId);

            if (!currentUserCanEdit)
            {
                return Result.Fail<NoteResponse>("No tienes permiso para editar esta nota");
            }

            var note = await _repository.GetNoteByIdAsync(request.NoteId);

            if (note is null)
            {
                return Result.Fail<NoteResponse>("Nota no encontrada o no se pudo actualizar");
            }
            note.IsPublished = !note.IsPublished;
            note.UpdateAt = DateTime.UtcNow;
            if (note.IsPublished)
            {
                note.PublishedAt = DateTime.UtcNow;
            }
            var updatedNote = await _repository.UpdateNote(note);

            if (updatedNote is null)
            {
                return Result.Fail<NoteResponse>("Nota no encontrada o no se pudo actualizar");
            }

            return updatedNote.Adapt<NoteResponse>();
        }
    }
}
