namespace Autorent.Infrastructure.Persistence.Seeders
{
    public static class SeedData
    {
        public static async Task Initialize(ApplicationDbContext db)
        {
            await CarSeeder.Seed(db);
        }
    }
}
