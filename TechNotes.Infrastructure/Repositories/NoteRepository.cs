using Microsoft.EntityFrameworkCore;
using TechNotes.Application.Notes;
using TechNotes.Domain.Notes;

namespace TechNotes.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;

        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<Note?> GetNoteByIdAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task<Note?> UpdateNote(Note note)
        {
            var noteUpdate = await GetNoteByIdAsync(note.Id);

            if (noteUpdate is null)
            {
                return null;
            }

            noteUpdate.Title = note.Title;
            noteUpdate.Content = note.Content;
            noteUpdate.IsPublished = note.IsPublished;
            noteUpdate.PublishedAt = note.PublishedAt;
            noteUpdate.UpdateAt = DateTime.UtcNow;

            _context.Notes.Update(noteUpdate);
            await _context.SaveChangesAsync();

            return noteUpdate;
        }

        public async Task<bool> DeleteNoteAsync(int id)
        {
            var noteDelete = await GetNoteByIdAsync(id);

            if(noteDelete is null)
            {
                return false;
            }

            _context.Notes.Remove(noteDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
