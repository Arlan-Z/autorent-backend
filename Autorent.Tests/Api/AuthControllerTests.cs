using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Autorent.Tests.Api
{
    public class AuthControllerTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AuthControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_ReturnsToken()
        {
            await _client.PostAsJsonAsync("/api/auth/register", new
            {
                name = "Test",
                email = "test@test.com",
                password = "123456"
            });

            var response = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email = "test@test.com",
                password = "123456"
            });

            var token = await response.Content.ReadAsStringAsync();

            Assert.False(string.IsNullOrEmpty(token));
        }
    }
}

