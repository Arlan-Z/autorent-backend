using Autorent.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Autorent.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsEnvironment("Testing"))
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("AutorentTestDb");
                });
            }
            else
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(
                        builder.Configuration.GetConnectionString("DefaultConnection")
                    );
                });
            }
        }
    }
}
