using Autorent.Infrastructure.Persistence;
using Autorent.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Autorent.Tests.Services
{
    public class AuthServiceTests
    {
        private ApplicationDbContext CreateDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        private IConfiguration CreateConfig()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Jwt:Key"] = "THIS_IS_SUPER_SECRET_KEY_123456",
                    ["Jwt:Issuer"] = "test",
                    ["Jwt:Audience"] = "test"
                })
                .Build();
        }

        [Fact]
        public async Task Register_CreatesUser()
        {
            var db = CreateDb();
            var config = CreateConfig();

            var service = new AuthService(db, config);

            var result = await service.Register(
                "Test",
                "test@mail.com",
                "password"
            );

            Assert.Equal("User created", result);
            Assert.Single(db.Users);
        }
    }

}
