using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Autorent.Tests.Api
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public CustomWebApplicationFactory()
        {
            Environment.SetEnvironmentVariable(
                "Jwt__Key",
                "SUPER_SECRET_TEST_KEY_MUST_BE_LONG_ENOUGH_12345"
            );
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
        }
    }
}
