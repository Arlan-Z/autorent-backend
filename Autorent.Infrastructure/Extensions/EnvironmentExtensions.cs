using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Autorent.Infrastructure.Extensions
{
    public static class EnvironmentExtensions
    {
        public static void AddEnvironment(this WebApplicationBuilder builder)
        {
            Env.Load();
            builder.Configuration.AddEnvironmentVariables();
        }
    }
}
