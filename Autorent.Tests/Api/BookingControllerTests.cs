using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace Autorent.Tests.Api
{
    public class BookingControllerTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public BookingControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> RegisterAndLoginAsync()
        {
            var email = $"test+{Guid.NewGuid()}@test.com";

            await _client.PostAsJsonAsync("/api/auth/register", new
            {
                name = "Test User",
                email,
                password = "123456"
            });

            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email,
                password = "123456"
            });

            loginResponse.EnsureSuccessStatusCode();

            var json = await loginResponse.Content.ReadFromJsonAsync<JsonElement>();

            return json.GetProperty("token").GetString()!;
        }


        private void SetAuthHeader(string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        [Fact]
        public async Task GetMyBookings_ReturnsUnauthorized_WhenNoToken()
        {
            var response = await _client.GetAsync("/api/booking/my");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetMyBookings_ReturnsOk_WhenAuthorized()
        {
            var token = await RegisterAndLoginAsync();
            SetAuthHeader(token);

            var response = await _client.GetAsync("/api/booking/my");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateBooking_ReturnsOk_WhenDataIsValid()
        {
            var token = await RegisterAndLoginAsync();
            SetAuthHeader(token);

            var carResponse = await _client.PostAsJsonAsync("/api/cars", new
            {
                brand = "BMW",
                model = "X5",
                year = 2022,
                priceHour = 10,
                priceDay = 200,
            });

            carResponse.EnsureSuccessStatusCode();

            var carJson = await carResponse.Content.ReadFromJsonAsync<JsonElement>();
            var carId = carJson.GetProperty("id").GetInt32();

            var bookingRequest = new
            {
                carId,
                startDate = DateTime.UtcNow.AddHours(1),
                endDate = DateTime.UtcNow.AddHours(3)
            };

            var response = await _client.PostAsJsonAsync("/api/booking", bookingRequest);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CheckAvailable_ReturnsTrue_WhenNoBookings()
        {
            var token = await RegisterAndLoginAsync();
            SetAuthHeader(token);

            var start = DateTime.UtcNow.AddHours(5);
            var end = DateTime.UtcNow.AddHours(7);

            var response = await _client.GetAsync(
                $"/api/booking/available?carId=1&start={start:o}&end={end:o}"
            );

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            Assert.Contains("true", result.ToLower());
        }

        [Fact]
        public async Task CancelBooking_ReturnsNotFound_ForInvalidId()
        {
            var token = await RegisterAndLoginAsync();

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsync(
                "/api/booking/999/cancel",
                null
            );

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
