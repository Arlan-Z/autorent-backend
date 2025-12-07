using Autorent.Infrastructure.Persistence;
using Autorent.Infrastructure.Persistence.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Autorent.Infrastructure.Extensions
{
    public static class SeederExtensions
    {
        public static async Task UseSeeders(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
                return;

            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            Console.WriteLine("🌱 Running seeders (Development only)...");
            await SeedData.Initialize(db);
        }
    }
}
