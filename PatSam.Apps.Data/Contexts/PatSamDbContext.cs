using Microsoft.EntityFrameworkCore;
using PatSam.Apps.Data.Entities;

namespace PatSam.Apps.Data.Contexts
{
    public class PatSamDbContext: DbContext
    {

        public PatSamDbContext(DbContextOptions<PatSamDbContext> options): base(options)
        {
        }

        // Entities
        public DbSet<Employee> Employee { get; set; }
    }
}
