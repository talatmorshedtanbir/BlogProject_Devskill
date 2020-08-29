using System.Threading.Tasks;

namespace BlogProject_Devskill.Data
{
    public interface ISeed
    {
        Task MigrateAsync();
        Task SeedAsync();
    }
}
