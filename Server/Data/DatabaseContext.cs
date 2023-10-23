using Microsoft.EntityFrameworkCore;
using STGeneticsTechTestLeonardoMosquera.Shared.Models;

namespace STGeneticsTechTestLeonardoMosquera.Server.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { 
        
        }

        public DbSet<Animal> Animal { get; set; }
    }
}
