

namespace TechNotes.Application.Notes.UpdateNote
{
    public class UpdateNoteCommandHandler : ICommandHandler<UpdateNoteCommand, NoteResponse?>
    {
        private readonly INoteRepository _noteRepository;

        public UpdateNoteCommandHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Result<NoteResponse?>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var noteUpdate = request.Adapt<Note>();

            var updatedNote = await _noteRepository.UpdateNote(noteUpdate);

            if (updatedNote is null)
            {
                return Result.Fail<NoteResponse?>("Nota no encontrada o no se pudo actualizar");
            }

            return updatedNote.Adapt<NoteResponse>();
        }
    }
}
