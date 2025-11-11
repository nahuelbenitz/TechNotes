using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechNotes.Infrastructure
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer("Server=IDEAPAD-NAHUEL;Database=TechNotesDb;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true",
                b => b.MigrationsAssembly("TechNotes.Infrastructure"));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
