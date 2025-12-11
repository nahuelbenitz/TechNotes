namespace TechNotes.Application.Notes
{
    public interface INoteRepository
    {
        Task<List<Note>> GetAllNotesAsync();
        Task<List<Note>> GetNotesByUserAsync(string userId);
        Task<Note?> GetNoteByIdAsync(int id);
        Task<Note> CreateNoteAsync(Note note);
        Task<Note?> UpdateNote(Note note);
        Task<bool> DeleteNoteAsync(int id);
    }
}
