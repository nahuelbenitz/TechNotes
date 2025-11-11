using TechNotes.Domain.Notes;

namespace TechNotes.Application.Notes
{
    public interface INoteRepository
    {
        Task<List<Note>> GetAllNotesAsync();
    }
}
