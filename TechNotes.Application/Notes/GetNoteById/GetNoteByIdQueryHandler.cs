using Mapster;
using MediatR;

namespace TechNotes.Application.Notes.GetNoteById
{
    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteResponse?>
    {
        private readonly INoteRepository _noteRepository;

        public GetNoteByIdQueryHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<NoteResponse?> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetNoteByIdAsync(request.Id);

            if (note is null)
            {
                return null;
            }
            
            return note.Adapt<NoteResponse>();


        }
    }
}
