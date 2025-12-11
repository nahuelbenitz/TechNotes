using MediatR;
using System.ComponentModel.DataAnnotations;
using TechNotes.Application.Notes;
using TechNotes.Application.Notes.GetNotesByCurrentUser;
using TechNotes.Application.Notes.TogglePublishNote;

namespace TechNotes.Application.Users
{
    public class NotesOverviewService : INotesOverviewService
    {
        private readonly ISender _sender;

        public NotesOverviewService(ISender sender)
        {
            _sender = sender;
        }

        public async Task<List<NoteResponse>?> GetNoteByCurrentUserAsync()
        {
            var result = await _sender.Send(new GetNotesByCurrentUserQuery());
            return result;
        }

        public async Task<NoteResponse?> TogglePublishNoteAsync(int noteId)
        {
            var result = await _sender.Send(new TogglePublishNoteCommand { NoteId = noteId });
            return result;
        }
    }
}
