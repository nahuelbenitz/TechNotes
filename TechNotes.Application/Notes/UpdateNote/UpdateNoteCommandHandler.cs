

using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.UpdateNote
{
    public class UpdateNoteCommandHandler : ICommandHandler<UpdateNoteCommand, NoteResponse?>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserService _userService;

        public UpdateNoteCommandHandler(INoteRepository noteRepository, IUserService userService)
        {
            _noteRepository = noteRepository;
            _userService = userService;
        }

        public async Task<Result<NoteResponse?>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var noteUpdate = request.Adapt<Note>();
            var currentUserCanEdit = await _userService.CurrentUserCanUpdateNoteAsync(request.Id);

            if (!currentUserCanEdit)
            {
                return Result.Fail<NoteResponse?>("No tienes permiso para editar esta nota");
            }

            var updatedNote = await _noteRepository.UpdateNote(noteUpdate);

            if (updatedNote is null)
            {
                return Result.Fail<NoteResponse?>("Nota no encontrada o no se pudo actualizar");
            }

            return updatedNote.Adapt<NoteResponse>();
        }
    }
}
