using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Data
{
    public abstract class DataSeed : ISeed
    {
        protected readonly DbContext _context;
        public DataSeed(DbContext context)
        {
            _context = context;
        }

        public async Task MigrateAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }

        public abstract Task SeedAsync();
    }
}
