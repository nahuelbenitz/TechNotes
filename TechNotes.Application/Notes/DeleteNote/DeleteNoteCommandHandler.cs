namespace TechNotes.Application.Notes.DeleteNote
{
    public class DeleteNoteCommandHandler : ICommandHandler<DeleteNoteCommand>
    {
        private readonly INoteRepository _noteRepository;

        public DeleteNoteCommandHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Result> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _noteRepository.DeleteNoteAsync(request.Id);

            if (!deleted)
            {
                return Result.Fail("Nota no se pudo eliminar o no se encontro");
            }

            return Result.Ok();
        }
    }
}
