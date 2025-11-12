using Mapster;
using MediatR;
using TechNotes.Domain.Notes;

namespace TechNotes.Application.Notes.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteResponse?>
    {
        private readonly INoteRepository _noteRepository;

        public UpdateNoteCommandHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<NoteResponse?> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var noteUpdate = request.Adapt<Note>();

            var updatedNote = await _noteRepository.UpdateNote(noteUpdate);

            if (updatedNote is null)
            {
                return null;
            }

            return updatedNote.Adapt<NoteResponse>();
        }
    }
}
